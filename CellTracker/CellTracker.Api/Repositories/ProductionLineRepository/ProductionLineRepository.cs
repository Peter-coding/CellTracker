using CellTracker.Api.Data;
using CellTracker.Api.Models;
using CellTracker.Api.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CellTracker.Api.Repositories.ProductionLineRepository
{
    public class ProductionLineRepository : Repository<ProductionLine>, IProductionLineRepository
    {
        
        public ProductionLineRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Cell> AddNextCellToProductionLine(CellDto cellDto, Guid productionLineId)
        {
            Cell cell = new Cell
            {
                Name = cellDto.Name,
                Description = cellDto.Description,
                ProductionLineId = productionLineId
            };
            cell.OrdinalNumber = GetNextOrdinalNumberForCellOnProductionLine(productionLineId);
            cell.Id = Guid.NewGuid();
            await _dbContext.Cells.AddAsync(cell);
            return cell;
        }

        private int GetNextOrdinalNumberForCellOnProductionLine(Guid productionLineId)
        {
            var cellsOnProductionLine = _dbContext.Cells.Where(c => c.ProductionLineId == productionLineId);
            if (!cellsOnProductionLine.Any())
            {
                return 1;
            }
            return cellsOnProductionLine.Max(c => c.OrdinalNumber) + 1;
        }
    }
}
