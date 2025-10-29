using CellTracker.Api.Data;
using CellTracker.Api.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Repositories
{
    public class CellRepository : Repository<Cell>
    {
        public CellRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
