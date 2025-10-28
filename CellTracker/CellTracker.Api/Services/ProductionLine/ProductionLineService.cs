using CellTracker.Api.Models;
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
