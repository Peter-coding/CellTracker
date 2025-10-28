using CellTracker.Api.Data;
using CellTracker.Api.Models;

namespace CellTracker.Api.Repositories
{
    public class WorkStationRepository : Repository<WorkStation>
    {
        public WorkStationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
