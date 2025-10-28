using CellTracker.Api.Data;
using CellTracker.Api.Models;

namespace CellTracker.Api.Repositories.WorkStationRepository
{
    public class WorkStationRepository : Repository<WorkStation>, IWorkStationRepository
    {
        public WorkStationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
