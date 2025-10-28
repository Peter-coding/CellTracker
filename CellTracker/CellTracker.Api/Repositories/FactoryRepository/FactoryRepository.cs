using CellTracker.Api.Data;
using CellTracker.Api.Models;

namespace CellTracker.Api.Repositories.FactoryRepository
{
    public class FactoryRepository : Repository<Factory>, IFactoryRepository
    {
        public FactoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        //Here we can override functions later if needed
    }
}
