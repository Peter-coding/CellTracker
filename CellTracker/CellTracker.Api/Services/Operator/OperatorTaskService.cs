using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Repositories;

namespace CellTracker.Api.Services.OperatorTaskService
{
    //Business logic layer for OperatorTask related tasks.
    public class OperatorTaskService : IOperatorTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OperatorTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public OperatorTask AddOperatorTask(OperatorTask task)
        {
            _unitOfWork.OperatorTaskRepository.Add(task);
            _unitOfWork.CompleteAsync();
            return task;
        }

        public IQueryable<OperatorTask> GetAllOperatorTasks()
        {
            return _unitOfWork.OperatorTaskRepository.GetAll();
        }

        public async Task<OperatorTask> GetOperatorTaskById(Guid id)
        {
            return await _unitOfWork.OperatorTaskRepository.GetByIdAsync(id);
        }

        public async void RemoveOperatorTaskById(Guid id)
        {
            _unitOfWork.OperatorTaskRepository.RemoveById(id);
            await _unitOfWork.CompleteAsync();
        }

        public void UpdateOperatorTask(OperatorTask task)
        {
            _unitOfWork.OperatorTaskRepository.Update(task);
            _unitOfWork.CompleteAsync();
        }
    }
}
