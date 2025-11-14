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

        public async Task<ProductionLine> AddProductionLine(CreateProductionLineDto productionLineDto)
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
                FactoryId = productionLineDto.FactoryId,
                OrdinalNumber = await GetNextOrdinalNumberForProdLineInFactory(productionLineDto.FactoryId),
                CreatedAt = DateTime.UtcNow
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

        public async Task<ProductionLine> UpdateProductionLine(UpdateProductionLineDto productionLineDto)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(productionLineDto.Id);
            //TODO add automapper
            productionLine.Name = productionLineDto.Name;
            productionLine.Description = productionLineDto.Description;
            productionLine.IsDeleted = productionLineDto.IsDeleted;
            productionLine.FactoryId = productionLineDto.FactoryId;
            productionLine.OrdinalNumber = productionLineDto.OrdinalNumber;
            productionLine.Status = productionLineDto.Status;
            productionLine.ModifiedAt = DateTime.UtcNow;

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

        public async Task<IEnumerable<Cell>> GetCellsInProdLine(Guid id)
        {
            var productionLine = await _unitOfWork.ProductionLineRepository.GetByIdAsync(id);
            if (productionLine == null)
            {
                throw new ArgumentException("Production Line not found");
            }
            var cells = await _unitOfWork.CellRepository.GetAll()
                .Where(c => c.ProductionLineId == id)
                .ToListAsync();
            return cells;
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
