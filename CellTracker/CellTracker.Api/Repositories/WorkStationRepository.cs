using CellTracker.Api.Data;
using CellTracker.Api.Models.Configuration;

namespace CellTracker.Api.Repositories
{
    public class WorkStationRepository : Repository<WorkStation>
    {
        public WorkStationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
