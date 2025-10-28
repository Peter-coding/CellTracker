using CellTracker.Api.Models;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.ProductionLineService
{
    public interface IProductionLineService
    {
        public ProductionLine AddProductionLine(ProductionLine productionLine);
        public IQueryable<ProductionLine> GetAllProductionLines();
        public Task<ProductionLine> GetProductionLineById(Guid id);
        public void RemoveProductionLineById(Guid id);
        public void UpdateProductionLine(ProductionLine productionLine);
    }
}
