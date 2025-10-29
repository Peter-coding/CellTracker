using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Repositories;

namespace CellTracker.Api.Services.FactoryService
{
    public class FactoryService : IFactoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FactoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public Factory AddFactory(Factory factory)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Factory> GetAllFactories()
        {
            throw new NotImplementedException();
        }

        public Task<Factory> GetFactoryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveFactoryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateFactory(Factory factory)
        {
            throw new NotImplementedException();
        }
    }
}
