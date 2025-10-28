using CellTracker.Api.Models;
using CellTracker.Api.Models.Dto;

namespace CellTracker.Api.Repositories.ProductionLineRepository
{
    public interface IProductionLineRepository : IRepository<ProductionLine>
    {
        Task<Cell> AddNextCellToProductionLine(CellDto cellDto, Guid productionLineId);
    }
}
