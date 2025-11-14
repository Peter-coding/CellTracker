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
            //ProdLine which is used for simulation
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(parameters.ProductionLineId);

            if (productionLine == null)
            {
                throw new ArgumentException($"Production line with ID {parameters.ProductionLineId} not found.");
            }

            //Cells of ProdLine
            var cells = await _productionLineService.GetCellsInProdLine(parameters.ProductionLineId);

            //Ordered for simulation
            var orderedCells = cells.OrderBy(c => c.OrdinalNumber).ToList();

            List<WorkStation> workStations = [];
            foreach (var cell in orderedCells)
            {
                var ws = await _cellService.GetWorkStationsOfCellAsync(cell.Id);
                if (ws != null)
                {
                    //WorkStations for simulation ordered by OrdinalNumber but ordered only within a cell
                    workStations.AddRange(ws.OrderBy(w => w.OrdinalNumber));
                }
            }

            var factory = new MqttClientFactory();

            //Client to send the messages
            var mqttClient = factory.CreateMqttClient();

            //How much time it takes to make a product
            var minutesOfOneProduct = (double)parameters.MinutesOfSimulation / parameters.NumberOfProductsMade;
            //Time each workstation has to process a product
            var workStationPeriod = (double)minutesOfOneProduct / workStations.Count();

            //Timer to simulate work speed. Each tick represents the time a workstation has to process a product
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(workStationPeriod));

            //All messages that will be sent during the simulation
            var wsMessagesCount = parameters.NumberOfProductsMade * workStations.Count();

            //Ofc starting with zero
            int currentMessagesCount = 0;

            //Index of current workstation that is processing a product. It's like round robin. Each WS is used once per product
            int currentWorkStationIndex = 0;

            //Deciding which shift the simulation is for. It's the starting time.
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

            //Just for undefined values
            Random rnd = new Random();

            //Calculating how much time each message represents within an 8 hour shift
            var shiftLength = TimeSpan.FromHours(8);
            var interval = TimeSpan.FromTicks(shiftLength.Ticks / wsMessagesCount);

            //Sending messages in a loop until all messages are sent
            while (currentMessagesCount < wsMessagesCount)
            {
                //If all workstations have processed the current product, start again from the first workstation
                if (currentWorkStationIndex >= workStations.Count())
                {
                    currentWorkStationIndex = 0;
                } 
                
                await timer.WaitForNextTickAsync();

                //WorkStation which will send data
                var currentWorkStation = workStations.ElementAt(currentWorkStationIndex);
                //Data that the WS will send
                var telemetryData = new TelemetryData
                {
                    TimeStamp = startTime + interval * currentMessagesCount,
                    WorkStationId = workStations.ElementAt(currentWorkStationIndex).Id.ToString(),
                    IsCompleted = rnd.Next(0, 100) >= 97,
                    Error = 0,     
                    //TODO: Is this needed?
                    OperatorId = $"OP-{rnd.Next(100, 999)}", 
                    ProductId = $"PRD-{rnd.Next(1000, 9999)}"

                };

                //Creating MQTT message
                string payload = JsonSerializer.Serialize(telemetryData);
                var message = new MqttApplicationMessageBuilder()
                  .WithTopic("telemetry/test")
                  .WithPayload(payload)
                  .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                  .Build();

                //Connecting to MQTT broker
                while (!mqttClient.IsConnected)
                {
                    await mqttClient.ConnectAsync(_mqttOptions);
                }
                //Sending MQTT message
                await mqttClient.PublishAsync(message);

                //If the product is completed (no error), next WS is used.
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
