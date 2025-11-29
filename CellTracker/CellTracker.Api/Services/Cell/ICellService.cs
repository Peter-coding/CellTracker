using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Models.Statistics;

namespace CellTracker.Api.Services.CellService
{
    public interface ICellService
    {
        public Task<Cell> AddCell(CreateCellDto cellDt);
        public Task<IEnumerable<Cell>> GetAllCells();
        public Task<Cell> GetCellById(Guid id);
        public Task<bool> RemoveCellById(Guid id);
        public Task<Cell> UpdateCell(UpdateCellDto cellDto);
        public Task<IEnumerable<WorkStation>> GetWorkStationsOfCellAsync(Guid guid);
        public Task<QualityRatio> GetEfficiencyOfCell(Guid cellId);
        public Task<int> GetQuantityGoalOfCell(Guid cellId);
    }
}
