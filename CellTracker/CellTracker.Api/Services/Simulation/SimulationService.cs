using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.Simulation;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.CellService;
using CellTracker.Api.Services.ProductionLineService;
using InfluxDB.Client.Api.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using System.Text.Json;

namespace CellTracker.Api.Services.Simulation
{
    public class SimulationService : ISimulationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductionLineService _productionLineService;
        private readonly ICellService _cellService;
        private readonly MqttClientOptions _mqttOptions;
        public SimulationService(IUnitOfWork unitOfWork,
            IProductionLineService productionLineService,
            ICellService cellService,
            MqttClientOptions mqttOptions)
        {
            _unitOfWork = unitOfWork;
            _productionLineService = productionLineService;
            _cellService = cellService;
            _mqttOptions = mqttOptions;
        }
        public async Task<IResult> StartSimulation(CreateSimulationDto parameters)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(parameters.ProductionLineId);

            if (productionLine == null)
            {
                throw new ArgumentException($"Production line with ID {parameters.ProductionLineId} not found.");
            }

            var cells = await _productionLineService.GetCellsInProdLine(parameters.ProductionLineId);

            var orderedCells = cells.OrderBy(c => c.OrdinalNumber).ToList();

            List<WorkStation> workStations = [];
            foreach (var cell in orderedCells)
            {
                var ws = await _cellService.GetWorkStationsOfCellAsync(cell.Id);
                if (ws != null)
                {
                    workStations.AddRange(ws.OrderBy(w => w.OrdinalNumber));
                }
            }

            List<IMqttClient> mqttClients = [];

            var factory = new MqttClientFactory();

            var mqttClient = factory.CreateMqttClient();

            await mqttClient.ConnectAsync(_mqttOptions);

            var minutesOfOneProduct = (double)parameters.MinutesOfSimulation / parameters.NumberOfProductsMade;
            var workStationPeriod = (double)minutesOfOneProduct / workStations.Count();

            var timer = new PeriodicTimer(TimeSpan.FromMinutes(workStationPeriod));

            var wsMessagesCount = parameters.NumberOfProductsMade * workStations.Count();
            var currentMessagesCount = 0;

            int currentWorkStationIndex = 0;

            DateTime startTime;
            if (parameters.Shift == Shift.Morning)
            {
                startTime = DateTime.Today.AddHours(6);
            }
            else if (parameters.Shift == Shift.Afternoon)
            {
                startTime = DateTime.Today.AddHours(14); // 2:00 PM
            }
            else
            {
                startTime = DateTime.Today.AddHours(22); // 10:00 PM
            }

            Random rnd = new Random();

            var shiftLength = TimeSpan.FromHours(8);
            var interval = TimeSpan.FromTicks(shiftLength.Ticks / wsMessagesCount);

            while (currentMessagesCount < wsMessagesCount)
            {
                if(currentWorkStationIndex >= workStations.Count())
                {
                    currentWorkStationIndex = 0;
                } 
                
                await timer.WaitForNextTickAsync();

                var currentWorkStation = workStations.ElementAt(currentWorkStationIndex);
                var telemetryData = new TelemetryData
                {
                    TimeStamp = startTime + interval * currentMessagesCount,
                    WorkStationId = workStations.ElementAt(currentWorkStationIndex).Id.ToString(),
                    IsCompleted = rnd.Next(0, 2) == 1,
                    Error = 0,     
                    //TODO: Is this needed?
                    OperatorId = $"OP-{rnd.Next(100, 999)}", 
                    ProductId = $"PRD-{rnd.Next(1000, 9999)}"

                };

                string payload = JsonSerializer.Serialize(telemetryData);
                var message = new MqttApplicationMessageBuilder()
                  .WithTopic("telemetry/test")
                  .WithPayload(payload)
                  .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                  .Build();

                while (!mqttClient.IsConnected)
                {
                    await mqttClient.ConnectAsync(_mqttOptions);
                }
                    await mqttClient.PublishAsync(message);
                Console.WriteLine("Message sent: ", message.Payload.ToString());
                if (telemetryData.IsCompleted)
                {
                    currentMessagesCount++;
                    currentWorkStationIndex++;
                }
            }
            return Results.Ok();
        }

        public Task<IResult> StopSimulation(SimulationModel parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<SimulationModel> AddSimulation(CreateSimulationDto simulationDto)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(simulationDto.ProductionLineId);
            if (productionLine == null)
            {
                throw new ArgumentException("Production line not found");
            }

            SimulationModel simulationModel = new SimulationModel()
            {
                Id = Guid.NewGuid(),
                ProductionLineId = simulationDto.ProductionLineId,
                MinutesOfSimulation = simulationDto.MinutesOfSimulation,
                NumberOfProductsMade = simulationDto.NumberOfProductsMade,
                Shift = simulationDto.Shift,
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWork.SimulationRepository.Add(simulationModel);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }
            return simulationModel;
        }

        public async Task<IEnumerable<SimulationModel>> GetAllSimulations()
        {
            return await _unitOfWork.SimulationRepository.GetAll().ToListAsync();
        }

        public async Task<SimulationModel> GetSimulationById(Guid id)
        {
            return await _unitOfWork.SimulationRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveSimulationById(Guid id)
        {
            var item = await _unitOfWork.SimulationRepository.GetByIdAsync(id);

            if (item.IsDeleted == true)
            {
                return true;
            }

            _unitOfWork.SimulationRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            return count > 0;
        }

        public async Task<SimulationModel> UpdateSimulation(UpdateSimulationDto updateSimulationDto)
        {
            var simulationModel = await _unitOfWork.SimulationRepository.GetByIdAsync(updateSimulationDto.Id);

            if (simulationModel == null)
            {
                throw new ArgumentException("Simulation model not found");
            }

            simulationModel.MinutesOfSimulation = updateSimulationDto.MinutesOfSimulation;
            simulationModel.NumberOfProductsMade = updateSimulationDto.NumberOfProductsMade;
            simulationModel.Shift = updateSimulationDto.Shift;
            simulationModel.ProductionLineId = updateSimulationDto.ProductionLineId;
            simulationModel.ModifiedAt = DateTime.UtcNow;

            _unitOfWork.SimulationRepository.Update(simulationModel);

            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return simulationModel;
        }


    }
}
