using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Services.ProductionLineService
{
    public class ProductionLineService : IProductionLineService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductionLineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Cell> AddNextCellToProductionLine(CellDto cellDto, Guid productionLineId)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(productionLineId);
            if (productionLine == null) {
                throw new ArgumentException("Production line not found");
            }
            Cell cell = new Cell
            {
                Name = cellDto.Name,
                Description = cellDto.Description,
                ProductionLineId = productionLineId
            };
            cell.OrdinalNumber = await GetNextOrdinalNumberForCellOnProductionLine(productionLineId);
            cell.Id = Guid.NewGuid();
            _unitOfWork.CellRepository.Add(cell);

            return cell;
        }

        private async Task<int> GetNextOrdinalNumberForCellOnProductionLine(Guid productionLineId)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(productionLineId);
            if (productionLine!= null && !productionLine.Cells.Any())
            {
                return 1;
            }

            return productionLine.Cells.Max(c => c.OrdinalNumber) + 1;
        }

        public async Task<ProductionLine> AddProductionLine(ProductionLine productionLine)
        {
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
            return await _unitOfWork.ProductionLineRepository.GetAll().ToListAsync();
        }

        public async Task<ProductionLine> GetProductionLineById(Guid id)
        {
            return await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveProductionLineById(Guid id)
        {
            _unitOfWork.ProductionLineRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<ProductionLine> UpdateProductionLine(ProductionLine productionLine)
        {
            _unitOfWork.ProductionLineRepository.Update(productionLine);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return productionLine;
        }
    }
}
