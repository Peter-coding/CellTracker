using CellTracker.Api.Data;
using CellTracker.Api.Models.OperatorTask;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Repositories
{
    public class OperatorTaskRepository : Repository<OperatorTask>
    {

        public OperatorTaskRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        //Here we can override functions later
    }
}
