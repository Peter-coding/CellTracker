using CellTracker.Api.Data;
using CellTracker.Api.Models;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Repositories.CellRepository;
using CellTracker.Api.Repositories.FactoryRepository;
using CellTracker.Api.Repositories.ProductionLineRepository;
using CellTracker.Api.Repositories.WorkStationRepository;

namespace CellTracker.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IRepository<OperatorTask> OperatorTaskRepository { get; }
        public IFactoryRepository FactoryRepository { get; }
        public IProductionLineRepository ProductionLineRepository { get; }
        public ICellRepository CellRepository { get; }
        public IWorkStationRepository WorkStationRepository { get; }
        //Add here the new repositories later

        public UnitOfWork(AppDbContext dbContext,
            IRepository<OperatorTask> operatorTaskRepository,
            IFactoryRepository factoryRepository,
            IProductionLineRepository productionLineRepository,
            ICellRepository cellRepository,
            IWorkStationRepository workStationRepository
            )
        {
            _dbContext = dbContext;
            OperatorTaskRepository = operatorTaskRepository;
            FactoryRepository = factoryRepository;
            ProductionLineRepository = productionLineRepository;
            CellRepository = cellRepository;
            WorkStationRepository = workStationRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
