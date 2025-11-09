using CellTracker.Api.Data;
using CellTracker.Api.Models.Configuration;

namespace CellTracker.Api.Repositories
{
    public class FactoryRepository : Repository<Factory>
    {
        public FactoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        //Here we can override functions later if needed
    }
}
