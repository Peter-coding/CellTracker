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

        public async Task<Cell> AddCell(CellDto cellDto)
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
                CreatedAt = DateTime.Now
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

        public async Task<ICollection<WorkStation>> GetWorkStationsOfCellAsync(Guid guid)
        {
              return _unitOfWork.WorkStationRepository.GetAll().Where(ws => ws.CellId == guid).ToList();
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
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Cell> UpdateCell(Cell cell)
        {
            _unitOfWork.CellRepository.Update(cell);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return cell;
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
