using CellTracker.Api.Models;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.CellService
{
    public interface ICellService
    {
        public Cell AddCell(Cell cell);
        public IQueryable<Cell> GetAllCells();
        public Task<Cell> GetCellById(Guid id);
        public void RemoveCellById(Guid id);
        public void UpdateCell(Cell cell);
        public Task<ICollection<WorkStation>> GetWorkStationsOfCellAsync(Guid guid);
    }
}
