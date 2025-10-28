using CellTracker.Api.Data;
using CellTracker.Api.Models;

namespace CellTracker.Api.Repositories.ProductionLineRepository
{
    public class ProductionLineRepository : Repository<ProductionLine>, IProductionLineRepository
    {
        public ProductionLineRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        //Here we can override functions later if needed
    }
}
