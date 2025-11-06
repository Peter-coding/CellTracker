using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.FactoryService
{
    public interface IFactoryService
    {
        public Task<Factory> AddFactory(Factory factory);
        public Task<IEnumerable<Factory>> GetAllFactories();
        public Task<Factory> GetFactoryById(Guid id);
        public Task<bool> RemoveFactoryById(Guid id);
        public Task<Factory> UpdateFactory(Factory factory);
    }
}
