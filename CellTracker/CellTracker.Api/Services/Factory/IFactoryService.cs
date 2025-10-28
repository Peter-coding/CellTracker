using CellTracker.Api.Models;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.FactoryService
{
    public interface IFactoryService
    {
        public Factory AddFactory(Factory factory);
        public IQueryable<Factory> GetAllFactories();
        public Task<Factory> GetFactoryById(Guid id);
        public void RemoveFactoryById(Guid id);
        public void UpdateFactory(Factory factory);
    }
}
