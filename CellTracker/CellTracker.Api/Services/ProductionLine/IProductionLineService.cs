using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.ProductionLineService
{
    public interface IProductionLineService
    {
        public Task<ProductionLine> AddProductionLine(ProductionLineDto productionLineDto);
        public Task<IEnumerable<ProductionLine>> GetAllProductionLines();
        public Task<ProductionLine> GetProductionLineById(Guid id);
        public Task<bool> RemoveProductionLineById(Guid id);
        public Task<ProductionLine> UpdateProductionLine(ProductionLine productionLine);
       public Task<bool> SetProductionLineStatus(Guid id, ProductionLineStatus status);
    }
}
