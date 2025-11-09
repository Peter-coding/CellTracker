using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.CellService
{
    public interface ICellService
    {
        public Task<Cell> AddCell(CreateCellDto cellDt);
        public Task<IEnumerable<Cell>> GetAllCells();
        public Task<Cell> GetCellById(Guid id);
        public Task<bool> RemoveCellById(Guid id);
        public Task<Cell> UpdateCell(UpdateCellDto cellDto);
        public Task<ICollection<WorkStation>> GetWorkStationsOfCellAsync(Guid guid);
    }
}
