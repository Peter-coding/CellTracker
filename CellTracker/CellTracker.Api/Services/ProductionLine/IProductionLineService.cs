using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.ProductionLineService
{
    public interface IProductionLineService
    {
        public Task<ProductionLine> AddProductionLine(ProductionLine productionLine);
        public Task<IEnumerable<ProductionLine>> GetAllProductionLines();
        public Task<ProductionLine> GetProductionLineById(Guid id);
        public Task<bool> RemoveProductionLineById(Guid id);
        public Task<ProductionLine> UpdateProductionLine(ProductionLine productionLine);
        public Task<Cell> AddNextCellToProductionLine(CellDto cellDto, Guid productionLineId);
    }
}
