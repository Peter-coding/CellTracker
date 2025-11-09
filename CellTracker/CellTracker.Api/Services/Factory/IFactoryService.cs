using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;

namespace CellTracker.Api.Services.FactoryService
{
    public interface IFactoryService
    {
        public Task<Factory> AddFactory(CreateFactoryDto factoryDto);
        public Task<IEnumerable<Factory>> GetAllFactories();
        public Task<Factory> GetFactoryById(Guid id);
        public Task<bool> RemoveFactoryById(Guid id);
        public Task<Factory> UpdateFactory(UpdateFactoryDto factoryDto);
    }
}
