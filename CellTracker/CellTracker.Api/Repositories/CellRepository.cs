using CellTracker.Api.Data;
using CellTracker.Api.Models;

namespace CellTracker.Api.Repositories
{
    public class CellRepository : Repository<Cell>
    {
        public CellRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        //Here we can override functions later if needed
    }
}
