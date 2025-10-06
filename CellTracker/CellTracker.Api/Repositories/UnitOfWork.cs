using CellTracker.Api.Data;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IRepository<OperatorTask> OperatorTaskRepository { get; }
        //Add here the new repositories later

        public UnitOfWork(AppDbContext dbContext, IRepository<OperatorTask> operatorTaskRepository)
        {
            _dbContext = dbContext;
            OperatorTaskRepository = operatorTaskRepository;
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
