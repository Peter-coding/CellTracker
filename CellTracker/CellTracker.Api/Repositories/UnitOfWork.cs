using CellTracker.Api.Data;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IRepository<OperatorTask> OperatorTaskRepository { get; }
        public IRepository<Factory> FactoryRepository { get; }
        public IRepository<ProductionLine> ProductionLineRepository { get; }
        public IRepository<Cell> CellRepository { get; }
        public IRepository<WorkStation> WorkStationRepository { get; }
        //Add here the new repositories later

        public UnitOfWork(AppDbContext dbContext,
            IRepository<OperatorTask> operatorTaskRepository,
            IRepository<Factory> factoryRepository,
            IRepository<ProductionLine> productionLineRepository,
            IRepository<Cell> cellRepository,
            IRepository<WorkStation> workStationRepository
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
