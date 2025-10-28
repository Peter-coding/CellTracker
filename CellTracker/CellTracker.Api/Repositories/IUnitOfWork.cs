using CellTracker.Api.Models;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Repositories.CellRepository;
using CellTracker.Api.Repositories.FactoryRepository;
using CellTracker.Api.Repositories.ProductionLineRepository;
using CellTracker.Api.Repositories.WorkStationRepository;

namespace CellTracker.Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<OperatorTask> OperatorTaskRepository { get; }
        IFactoryRepository FactoryRepository { get; }
        IProductionLineRepository ProductionLineRepository { get; }
        ICellRepository CellRepository { get; }
        IWorkStationRepository WorkStationRepository { get; }
        Task<int> CompleteAsync();
    }
}
