using CellTracker.Api.Data;
using CellTracker.Api.Models;

namespace CellTracker.Api.Repositories
{
    public class ProductionLineRepository : Repository<ProductionLine>
    {
        public ProductionLineRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        //Here we can override functions later if needed
    }
}
