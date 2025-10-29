using CellTracker.Api.Data;
using CellTracker.Api.Models.Base;
using CellTracker.Api.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        IQueryable<T> GetAll(CancellationToken cancellationToken = default);
        T Add(T entity);
        void RemoveById(Guid id);
        void Update(T entity);

    }
}