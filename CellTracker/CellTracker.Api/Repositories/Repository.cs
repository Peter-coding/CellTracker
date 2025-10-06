using CellTracker.Api.Data;
using CellTracker.Api.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        protected Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public virtual IQueryable<T> GetAll(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public virtual async void RemoveById(Guid id)
        {
            _dbContext.Set<T>().Remove(await GetByIdAsync(id));
        }

        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
