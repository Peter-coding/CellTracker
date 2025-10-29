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

        public ProductionLine AddProductionLine(ProductionLine productionLine)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductionLine> GetAllProductionLines()
        {
            throw new NotImplementedException();
        }

        public Task<ProductionLine> GetProductionLineById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductionLineById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductionLine(ProductionLine productionLine)
        {
            throw new NotImplementedException();
        }
    }
}
