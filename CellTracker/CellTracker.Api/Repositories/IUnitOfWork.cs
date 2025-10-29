using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.OperatorTask;
namespace CellTracker.Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<OperatorTask> OperatorTaskRepository { get; }
        IRepository<Factory> FactoryRepository { get; }
        IRepository<ProductionLine> ProductionLineRepository { get; }
        IRepository<Cell> CellRepository { get; }
        IRepository<WorkStation> WorkStationRepository { get; }
        Task<int> CompleteAsync();
    }
}
