using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.Statistics;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.CellService;
using CellTracker.Api.Services.TelemetryRepository;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Services.ProductionLineService
{
    public class ProductionLineService : IProductionLineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICellService _cellService;
        private readonly ITelemetryFetchService _telemetryFetchService;

        public ProductionLineService(IUnitOfWork unitOfWork, ICellService cellService, ITelemetryFetchService telemetryFetchService)
        {
            _unitOfWork = unitOfWork;
            _cellService = cellService;
            _telemetryFetchService =  telemetryFetchService;
        }

        public async Task<ProductionLine> AddProductionLine(CreateProductionLineDto productionLineDto)
        {
            var factory = await _unitOfWork.FactoryRepository.GetByIdAsync(productionLineDto.FactoryId);
            if (factory == null)
            {
                throw new ArgumentException("Factory not found");
            }
            ProductionLine productionLine = new ProductionLine
            {
                Name = productionLineDto.Name,
                Description = productionLineDto.Description,
                FactoryId = productionLineDto.FactoryId,
                OrdinalNumber = await GetNextOrdinalNumberForProdLineInFactory(productionLineDto.FactoryId),
                CreatedAt = DateTime.UtcNow
            };
            _unitOfWork.ProductionLineRepository.Add(productionLine);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return productionLine;
        }

        public async Task<IEnumerable<ProductionLine>> GetAllProductionLines()
        {
            return await _unitOfWork.ProductionLineRepository.GetAll().Where(line => line.IsDeleted != true).ToListAsync();
        }

        public async Task<ProductionLine> GetProductionLineById(Guid id)
        {
            return await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveProductionLineById(Guid id)
        {
            var item = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (item.IsDeleted == true)
            {
                return true;
            }
            _unitOfWork.ProductionLineRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<ProductionLine> UpdateProductionLine(UpdateProductionLineDto productionLineDto)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(productionLineDto.Id);
            //TODO add automapper
            productionLine.Name = productionLineDto.Name;
            productionLine.Description = productionLineDto.Description;
            productionLine.IsDeleted = productionLineDto.IsDeleted;
            productionLine.FactoryId = productionLineDto.FactoryId;
            productionLine.OrdinalNumber = productionLineDto.OrdinalNumber;
            productionLine.Status = productionLineDto.Status;
            productionLine.ModifiedAt = DateTime.UtcNow;

            _unitOfWork.ProductionLineRepository.Update(productionLine);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return productionLine;
        }

        public async Task<bool> SetProductionLineStatus(Guid id, ProductionLineStatus status)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (productionLine == null)
            {
                return false;
            }
            productionLine.Status = status;
            _unitOfWork.ProductionLineRepository.Update(productionLine);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Cell>> GetCellsInProdLine(Guid id)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (productionLine == null)
            {
                throw new ArgumentException("Production Line not found");
            }
            var cells = await _unitOfWork.CellRepository.GetAll()
                .Where(c => c.ProductionLineId == id)
                .ToListAsync();
            return cells;
        }

        public async Task<int> GetQuantityGoalInProdLine(Guid id)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (productionLine == null)
            {
                throw new ArgumentException("Production Line not found");
            }

            var cells = await GetCellsInProdLine(id);

           
            List<WorkStation> workStations = new List<WorkStation>();

            foreach (var cell in cells)
            {
                var ws = await _cellService.GetWorkStationsOfCellAsync(cell.Id);
                workStations.AddRange(ws);
            }

            int quantityGoal = 0;
            foreach (var ws in workStations)
            {
                if(ws.OperatorTask != null)
                {
                    quantityGoal += ws.OperatorTask.QuantityGoal;
                }
            }

            return quantityGoal;
        }

        public async Task<ProdLineQualityRatio> GetEfficiencyOfProdLine(Guid id)
        {
            var currentTime = DateTime.UtcNow;
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (productionLine == null)
            {
                throw new ArgumentException("Production Line not found");
            }
            
            var cells = await GetCellsInProdLine(id);
            List<WorkStation> workStations = new List<WorkStation>();
            foreach (var cell in cells)
            {
                var ws = await _cellService.GetWorkStationsOfCellAsync(cell.Id);
                workStations.AddRange(ws);
            }

      
            List<TelemetryData> telemetryData = new List<TelemetryData>();
            foreach (var ws in workStations)
            {
                telemetryData.AddRange(await _telemetryFetchService.GetTelemetryDataInCurrentShiftOfWorkStationAsync(ws.Id, currentTime));
            }

            int correct = 0;
            int defective = 0;

            foreach (var data in telemetryData)
            {
                if (data.Error != 0)
                {
                    defective++;
                }
                else
                {
                    correct++;
                }
            }

            ProdLineQualityRatio result = new ProdLineQualityRatio
            {
                CorrectProducts = correct,
                DefectiveProducts = defective
            };

            return result;
        }

        private async Task<int> GetNextOrdinalNumberForProdLineInFactory(Guid factoryId)
        {
            var factory = await _unitOfWork.FactoryRepository.GetByIdAsync(factoryId);

            var prodLines = await _unitOfWork.ProductionLineRepository.GetAll()
                .Where(pl => pl.FactoryId == factoryId)
                .ToListAsync();

            if (factory != null && !prodLines.Any())
            {
                return 1;
            }

            return prodLines.Max(pl => pl.OrdinalNumber) + 1;
        }

    }
}
