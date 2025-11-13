using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Simulation;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.CellService;
using CellTracker.Api.Services.ProductionLineService;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IResult> StartSimulation(SimulationParameters parameters)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(parameters.ProductionLineId);

            if (productionLine == null)
            {
                throw new ArgumentException($"Production line with ID {parameters.ProductionLineId} not found.");
            }

            var cells = await _productionLineService.GetCellsInProdLine(parameters.ProductionLineId);
            cells.OrderBy(c => c.OrdinalNumber);

            List<WorkStation> workStationsOfCells = [];
            foreach(var cell in cells)
            {
                var ws = await _cellService.GetWorkStationsOfCellAsync(cell.Id);
                if (ws != null)
                {
                    workStationsOfCells.AddRange(ws);
                }
            }

            var workStations  = workStationsOfCells.OrderBy(ws => ws.OrdinalNumber);
            List<IMqttClient> mqttClients = [];

            var factory = new MqttClientFactory();

            var mqttClient = factory.CreateMqttClient();

            await mqttClient.ConnectAsync(_mqttOptions);

            var minutesOfOneProduct = parameters.MinutesOfSimulation / parameters.NumberOfProductsMade;
            var workStationPeriod = minutesOfOneProduct / workStations.Count();
            
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

            while (currentMessagesCount < wsMessagesCount)
            {
                var currentWorkStation = workStations.ElementAt(currentWorkStationIndex);
                var telemetryData = new TelemetryData
                {
                    TimeStamp = startTime.AddMinutes(workStationPeriod),
                    WorkStationId = workStations.ElementAt(currentMessagesCount % workStations.Count()).Id.ToString(),
                    IsCompleted = rnd.Next(0, 2) == 1
                };

                string payload = JsonSerializer.Serialize(telemetryData);
                var message = new MqttApplicationMessageBuilder()
                  .WithTopic("telemetry/test")
                  .WithPayload(payload)
                  .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                  .Build();

                await mqttClient.PublishAsync(message);
                Console.WriteLine("Message sent: ", message.ToString());
                if (telemetryData.IsCompleted)
                {
                    currentMessagesCount++;
                    currentWorkStationIndex++;
                }
            }
            return Results.Ok();
        }
    }
}
