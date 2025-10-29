using CellTracker.Api.Data;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CellTracker.Api.Repositories
{
    public class ProductionLineRepository : Repository<ProductionLine>
    {
        
        public ProductionLineRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
