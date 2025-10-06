using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<OperatorTask> OperatorTaskRepository { get; }
        Task<int> CompleteAsync();
    }
}
