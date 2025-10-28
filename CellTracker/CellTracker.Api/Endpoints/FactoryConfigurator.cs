using CellTracker.Api.Models.Dto;
using CellTracker.Api.Services.ProductionLineService;

namespace CellTracker.Api.Endpoints
{
    public static class FactoryConfigurator
    {
        public static void MapFactoryConfiguratorEndpoints(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";

            app.MapPost($"{path}/AddNextCellToProductionLine", AddNextCellToProductionLineAsync);
        }

        public async static Task<IResult> AddNextCellToProductionLineAsync(IProductionLineService productionLineService, CellDto cellDto, Guid productionLineId)
        {
            var cell = await productionLineService.AddNextCellToProductionLine(cellDto, productionLineId);
            return Results.Ok(cell);
        }
    }
}
