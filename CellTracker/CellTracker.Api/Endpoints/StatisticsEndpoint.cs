using CellTracker.Api.Services.ProductionLineService;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;

namespace CellTracker.Api.Endpoints
{
    public static class StatisticsEndpoint
    {
        public static void MapStatisticsEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";
            app.MapGet($"{path}/GetQuantityGoalOfProdLine", GetQuantityGoalOfProdLine);
            app.MapGet(path + $"/GetEfficiencyOfProdLine", GetEfficiencyOfProdLine);
        }
        
        public async static Task<IResult> GetQuantityGoalOfProdLine(IProductionLineService productionLineService, Guid prodLineId)
        {
            var queantityGoal = await productionLineService.GetQuantityGoalInProdLine(prodLineId);
            return Results.Ok(queantityGoal);
        }

        public async static Task<IResult> GetEfficiencyOfProdLine(IProductionLineService productionLineService, Guid prodLineId)
        {
            var efficiency = await productionLineService.GetEfficiencyOfProdLine(prodLineId);
            return Results.Ok(efficiency);
        }
    }
}
