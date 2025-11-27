using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.ProductionLineService
{
    public interface IProductionLineService
    {
        public Task<ProductionLine> AddProductionLine(CreateProductionLineDto productionLineDto);
        public Task<IEnumerable<ProductionLine>> GetAllProductionLines();
        public Task<ProductionLine> GetProductionLineById(Guid id);
        public Task<bool> RemoveProductionLineById(Guid id);
        public Task<ProductionLine> UpdateProductionLine(UpdateProductionLineDto productionLineDto);
        public Task<bool> SetProductionLineStatus(Guid id, ProductionLineStatus status);
        public Task<IEnumerable<Cell>> GetCellsInProdLine(Guid id);
        public Task<int> GetQuantityGoalInProdLine(Guid id);
    }
}
