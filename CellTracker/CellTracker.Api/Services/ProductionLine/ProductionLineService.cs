using CellTracker.Api.Models;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Repositories;

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
            var cell = await _unitOfWork.ProductionLineRepository.AddNextCellToProductionLine(cellDto, productionLineId);
            await _unitOfWork.CompleteAsync();
            return cell;
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
