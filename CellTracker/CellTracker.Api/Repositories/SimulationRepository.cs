using CellTracker.Api.Data;
using CellTracker.Api.Models.Simulation;

namespace CellTracker.Api.Repositories
{
    public class SimulationRepository : Repository<SimulationModel>
    {
        public SimulationRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
