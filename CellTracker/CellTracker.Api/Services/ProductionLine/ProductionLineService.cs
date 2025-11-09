using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Services.ProductionLineService
{
    public class ProductionLineService : IProductionLineService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductionLineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductionLine> AddProductionLine(ProductionLineDto productionLineDto)
        {
            var factory = await _unitOfWork.FactoryRepository.GetByIdAsync(productionLineDto.FactoryId);
            if (factory == null)
            {
                throw new ArgumentException("Factory not found");
            }
            ProductionLine productionLine = new ProductionLine
            {
                Name = productionLineDto.Name,
                Description = productionLineDto.Description,
                OrdinalNumber = await GetNextOrdinalNumberForProdLineInFactory(productionLineDto.FactoryId),
                CreatedAt = DateTime.Now
            };
            _unitOfWork.ProductionLineRepository.Add(productionLine);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return productionLine;
        }

        public async Task<IEnumerable<ProductionLine>> GetAllProductionLines()
        {
            return await _unitOfWork.ProductionLineRepository.GetAll().ToListAsync();
        }

        public async Task<ProductionLine> GetProductionLineById(Guid id)
        {
            return await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveProductionLineById(Guid id)
        {
            var item = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (item.IsDeleted == true)
            {
                return true;
            }
            _unitOfWork.ProductionLineRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<ProductionLine> UpdateProductionLine(ProductionLine productionLine)
        {
            _unitOfWork.ProductionLineRepository.Update(productionLine);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return productionLine;
        }

        public async Task<bool> SetProductionLineStatus(Guid id, ProductionLineStatus status)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (productionLine == null)
            {
                return false;
            }
            productionLine.Status = status;
            _unitOfWork.ProductionLineRepository.Update(productionLine);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }
            return true;
        }

        private async Task<int> GetNextOrdinalNumberForProdLineInFactory(Guid factoryId)
        {
            var factory = await _unitOfWork.FactoryRepository.GetByIdAsync(factoryId);
            if (factory != null && !factory.ProductionLines.Any())
            {
                return 1;
            }
            return factory.ProductionLines.Max(pl => pl.OrdinalNumber) + 1;
        }
    }
}
