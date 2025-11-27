using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.OperatorTaskService
{
    public interface IOperatorTaskService
    {
        public Task<OperatorTask> AddOperatorTask(CreateOperatorTaskDto taskDto);
        public IQueryable<OperatorTask> GetAllOperatorTasks();
        public Task<OperatorTask> GetOperatorTaskById(Guid id);
        public void RemoveOperatorTaskById(Guid id);
        public void UpdateOperatorTask(OperatorTask task);
    }
}
