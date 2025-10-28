using CellTracker.Api.Models;

namespace CellTracker.Api.Repositories.CellRepository
{
    public interface ICellRepository : IRepository<Cell>
    {
        public Task<List<WorkStation>> GetWorkStationsOfCell(Guid cellId);
    }
}
