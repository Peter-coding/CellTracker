using CellTracker.Api.Data;
using CellTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Repositories.CellRepository
{
    public class CellRepository : Repository<Cell>, ICellRepository
    {
        public CellRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<WorkStation>> GetWorkStationsOfCell(Guid cellId)
        {
            return await _dbContext.Cells
                .Where(c => c.Id == cellId)
                .SelectMany(c => c.WorkStations)
                .ToListAsync();
        }

    }
}
