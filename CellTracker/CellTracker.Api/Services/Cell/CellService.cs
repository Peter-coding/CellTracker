using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CellTracker.Api.Services.CellService
{
    public class CellService : ICellService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CellService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return await _unitOfWork.CellRepository.GetAll().ToListAsync();
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
            return _unitOfWork.WorkStationRepository.GetAll().Where(ws => ws.CellId == id).ToList();
        }
        private async Task<int> GetNextOrdinalNumberForCellOnProductionLine(Guid productionLineId)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(productionLineId);
            if (productionLine != null && !productionLine.Cells.Any())
            {
                return 1;
            }
            return productionLine.Cells.Max(c => c.OrdinalNumber) + 1;
        }
    }
}
