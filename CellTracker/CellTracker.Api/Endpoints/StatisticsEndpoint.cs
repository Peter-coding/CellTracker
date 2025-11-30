using CellTracker.Api.Services.CellService;
using CellTracker.Api.Services.ProductionLineService;
using CellTracker.Api.Services.WorkStationService;
using InfluxDB.Client.Api.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;

namespace CellTracker.Api.Endpoints
{
    public static class StatisticsEndpoint
    {
        public static void MapStatisticsEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";
            app.MapGet($"{path}/GetQuantityGoalOfProdLine/{{prodLineId}}", GetQuantityGoalOfProdLine);
            app.MapGet($"{path}/GetQuantityGoalOfCell/{{cellId}}", GetQuantityGoalOfCell);
           
            app.MapGet(path + $"/GetEfficiencyOfProdLineCurrentShift/{{prodLineId}}", GetEfficiencyOfProdLineCurrentShift);
            app.MapGet($"{path}/GetEfficiencyOfCellCurrentShift/{{cellId}}", GetEfficiencyOfCellCurrentShift);
            app.MapGet($"{path}/GetEfficiencyOfWorkStationCurrentShift/{{wsId}}", GetEfficiencyOfWorkStationCurrentShift);
            app.MapGet($"{path}/GetOperatorEfficiencyPerHourCurrentShift/{{cellId}}", GetOperatorEfficiencyPerHourCurrentShift);
        }
        
        public async static Task<IResult> GetQuantityGoalOfProdLine(IProductionLineService productionLineService, Guid prodLineId)
        {
            var queantityGoal = await productionLineService.GetQuantityGoalInProdLine(prodLineId);
            return Results.Ok(queantityGoal);
        }

        public async static Task<IResult> GetQuantityGoalOfCell(ICellService cellService, Guid cellId)
        {
            var queantityGoal = await cellService.GetQuantityGoalOfCell(cellId);
            return Results.Ok(queantityGoal);
        }

        public async static Task<IResult> GetEfficiencyOfProdLineCurrentShift(IProductionLineService productionLineService, Guid prodLineId)
        {
            var efficiency = await productionLineService.GetEfficiencyOfProdLine(prodLineId);
            return Results.Ok(efficiency);
        }

        public async static Task<IResult> GetEfficiencyOfCellCurrentShift(ICellService cellService, Guid cellId)
        {
            var efficiency = await cellService.GetEfficiencyOfCell(cellId);
            return Results.Ok(efficiency);
        }

        public async static Task<IResult> GetEfficiencyOfWorkStationCurrentShift(IWorkStationService workStationService, Guid wsId)
        {
            var efficiency = await workStationService.GetEfficiencyOfWorkStation(wsId);
            return Results.Ok(efficiency);
        }

        public async static Task<IResult> GetOperatorEfficiencyPerHourCurrentShift(ICellService cellService, Guid cellId)
        {
            var efficiency = await cellService.GetOperatorEfficiencyPerHourCurrentShift(cellId);
            return Results.Ok(efficiency);
        }
    }
}
