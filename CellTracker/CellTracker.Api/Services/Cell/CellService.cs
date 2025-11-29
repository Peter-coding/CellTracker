using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.Statistics;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.TelemetryRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CellTracker.Api.Services.CellService
{
    public class CellService : ICellService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITelemetryFetchService _telemetryFetchService;
        public CellService(IUnitOfWork unitOfWork, ITelemetryFetchService telemetryFetchService)
        {
            _unitOfWork = unitOfWork;
            _telemetryFetchService = telemetryFetchService;
        }

        public async Task<Cell> AddCell(CreateCellDto cellDto)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(cellDto.ProductionLineId);
            if (productionLine == null)
            {
                throw new ArgumentException("Production line not found");
            }

            Cell cell = new Cell
            {
                Name = cellDto.Name,
                Description = cellDto.Description,
                ProductionLineId = cellDto.ProductionLineId,
                OrdinalNumber = await GetNextOrdinalNumberForCellOnProductionLine(cellDto.ProductionLineId),
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWork.CellRepository.Add(cell);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }
            return cell;
        }

        public async Task<IEnumerable<Cell>> GetAllCells()
        {
            return await _unitOfWork.CellRepository.GetAll().Where(cell => cell.IsDeleted != true).ToListAsync();
        }

        public async Task<Cell> GetCellById(Guid id)
        {
            return await _unitOfWork.CellRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveCellById(Guid id)
        {
            var item = await _unitOfWork.CellRepository.GetByIdAsync(id);
            if (item.IsDeleted == true)
            {
                return true;
            }
            _unitOfWork.CellRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            return count > 0;
        }

        public async Task<Cell> UpdateCell(UpdateCellDto cellDto)
        {
            var cell = await _unitOfWork.CellRepository.GetByIdAsync(cellDto.Id);

            if(cell == null)
            {
                throw new ArgumentException("Cell not found");
            }

            //TODO add automapper
            cell.Name = cellDto.Name;
            cell.Description = cellDto.Description;
            cell.ProductionLineId = cellDto.ProductionLineId;
            cell.OrdinalNumber = cellDto.OrdinalNumber;
            cell.ModifiedAt = DateTime.UtcNow;
            cell.IsDeleted = cellDto.IsDeleted;
           
            _unitOfWork.CellRepository.Update(cell);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return cell;
        }

        public async Task<IEnumerable<WorkStation>> GetWorkStationsOfCellAsync(Guid id)
        {
            var cell = await _unitOfWork.CellRepository.GetByIdAsync(id);
            if (cell == null)
            {
                throw new ArgumentException("Cell not found");
            }
            return _unitOfWork.WorkStationRepository
                    .GetAll()
                    .Include(ws => ws.OperatorTask)
                    .Where(ws => ws.CellId == id && ws.IsDeleted != true)
                    .ToList();
        }

        public async Task<QualityRatio> GetEfficiencyOfCell(Guid cellId)
        {
            var currentTime = DateTime.UtcNow;

            var cell = await _unitOfWork.CellRepository.GetByIdAsync(cellId);
            if (cell == null)
            {
                throw new ArgumentException("Cell not found");
            }

            var workStations = await GetWorkStationsOfCellAsync(cellId);

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

            QualityRatio result = new QualityRatio
            {
                CorrectProducts = correct,
                DefectiveProducts = defective
            };

            return result;
        }

        public async Task<int> GetQuantityGoalOfCell(Guid cellId)
        {
            var workStations = await GetWorkStationsOfCellAsync(cellId);
            int quantityGoal = 0;
            foreach (var ws in workStations)
            {
                if (ws.OperatorTask != null)
                {
                    quantityGoal += ws.OperatorTask.QuantityGoal;
                }
            }

            return quantityGoal;
        }

        private async Task<int> GetNextOrdinalNumberForCellOnProductionLine(Guid productionLineId)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(productionLineId);
            var cells = await _unitOfWork.CellRepository.GetAll()
                .Where(c => c.ProductionLineId == productionLineId)
                .ToListAsync();

            if (productionLine != null && !cells.Any())
            {
                return 1;
            }
            return cells.Max(c => c.OrdinalNumber) + 1;
        }
    }
}
