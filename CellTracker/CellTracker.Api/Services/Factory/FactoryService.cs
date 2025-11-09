using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Repositories;
using InfluxDB.Client.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Services.FactoryService
{
    public class FactoryService : IFactoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FactoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Factory> AddFactory(Factory factory)
        {
            _unitOfWork.FactoryRepository.Add(factory);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }
            return factory;
        }

        public async Task<IEnumerable<Factory>> GetAllFactories()
        {
            return await _unitOfWork.FactoryRepository.GetAll().ToListAsync();
        }

        public async Task<Factory> GetFactoryById(Guid id)
        {
            return await _unitOfWork.FactoryRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveFactoryById(Guid id)
        {
            var item = await _unitOfWork.FactoryRepository.GetByIdAsync(id);
            if (item.IsDeleted == true)
            {
                return true;
            }
            _unitOfWork.FactoryRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Factory> UpdateFactory(Factory factory)
        {
            _unitOfWork.FactoryRepository.Update(factory);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return factory;
        }
    }
}
