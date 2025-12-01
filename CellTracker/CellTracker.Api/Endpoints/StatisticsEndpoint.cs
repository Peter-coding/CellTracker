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

        //WorkStations have QuantityGoals via OperatorTasks.
        //Adding All QuantityGoals of a ProdLine/Cell/WorkStation
        public async static Task<IResult> GetQuantityGoalOfProdLine(IProductionLineService productionLineService, Guid prodLineId)
        {
            var queantityGoal = await productionLineService.GetQuantityGoalInProdLine(prodLineId);
            return Results.Ok(queantityGoal);
        }

        //QuantityGoal of a given Cell
        //Cell/WorkStation
        public async static Task<IResult> GetQuantityGoalOfCell(ICellService cellService, Guid cellId)
        {
            var queantityGoal = await cellService.GetQuantityGoalOfCell(cellId);
            return Results.Ok(queantityGoal);
        }

        //Efficiency of a ProdLine in the Current Shift
        //Returning QualityRatio in current shift of a given ProdLine
        //Number of completed products without errors and number of completed products with errors
        public async static Task<IResult> GetEfficiencyOfProdLineCurrentShift(IProductionLineService productionLineService, Guid prodLineId)
        {
            var efficiency = await productionLineService.GetEfficiencyOfProdLine(prodLineId);
            return Results.Ok(efficiency);
        }

        //Efficiency of a Cell in the Current Shift
        //Returning QualityRatio in current shift of a given Cell
        //Number of completed products without errors and number of completed products with errors
        public async static Task<IResult> GetEfficiencyOfCellCurrentShift(ICellService cellService, Guid cellId)
        {
            var efficiency = await cellService.GetEfficiencyOfCell(cellId);
            return Results.Ok(efficiency);
        }

        //Efficiency of a WorkStation in the Current Shift
        //Returning QualityRatio in current shift of a given WorkStation
        //Number of completed products without errors and number of completed products with errors
        public async static Task<IResult> GetEfficiencyOfWorkStationCurrentShift(IWorkStationService workStationService, Guid wsId)
        {
            var efficiency = await workStationService.GetEfficiencyOfWorkStation(wsId);
            return Results.Ok(efficiency);
        }

        //Returning Operator Efficiency Per Hour in Current Shift for a given Cell
        //In the list every Dictionary entry represents one hour (ordered)
        //In the dictionaty the key is the Operator Name and the value is the QualityRatio (number of correct and defective products)
        //So you can get the efficiency of every operator per hour in the current shift
        public async static Task<IResult> GetOperatorEfficiencyPerHourCurrentShift(ICellService cellService, Guid cellId)
        {
            var efficiency = await cellService.GetOperatorEfficiencyPerHourCurrentShift(cellId);
            return Results.Ok(efficiency);
        }
    }
}
