using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Simulation;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.CellService;
using CellTracker.Api.Services.ProductionLineService;
using MQTTnet;

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
        public async void StartSimulation(SimulationParameters parameters)
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

            var client = factory.CreateMqttClient();
            //options segítségével csatlakozás
            //üzenet küldés a megfelelő topicra round robin, megfelelő időközökkel
            //üzenetbe saját mqttdeviceid, timestamp, workstationid, siker/nem siker



        }
    }
}
