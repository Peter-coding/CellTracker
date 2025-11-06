using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.CellService
{
    public interface ICellService
    {
        public Task<Cell> AddCell(Cell cell);
        public Task<IEnumerable<Cell>> GetAllCells();
        public Task<Cell> GetCellById(Guid id);
        public Task<bool> RemoveCellById(Guid id);
        public Task<Cell> UpdateCell(Cell cell);
        public Task<ICollection<WorkStation>> GetWorkStationsOfCellAsync(Guid guid);
    }
}
