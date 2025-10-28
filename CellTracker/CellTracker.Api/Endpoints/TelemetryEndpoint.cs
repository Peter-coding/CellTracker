using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Services.OperatorTaskService;
using CellTracker.Api.Services.TelemetryRepository;

namespace CellTracker.Api.Endpoints
{
    public static class TelemetryEndpoint
    {
        public static void MapTelemetryEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";
     
            app.MapGet($"{path}/GetTelemetryDataBetween", GetTelemetryDataBetween);
            app.MapGet($"{path}/GetTelemetryDataInCurrentShift", GetTelemetryDataInCurrentShift);
            app.MapGet($"{path}/GetTelemetryCountInCurrentShift", GetTelemetryCountInCurrentShift);
        }
        public async static Task<IResult> GetTelemetryDataBetween(ITelemetryFetchService telemetryFetchService, DateTime from, DateTime to)
        {
            if (from > to)
            {
                var tmp = to;
                to = from;
                from = tmp;
            }
            var data = await telemetryFetchService.GetBetweenAsync(from, to);
            return Results.Ok(data);
        }

        public async static Task<IResult> GetTelemetryDataInCurrentShift(ITelemetryFetchService telemetryFetchService, string operatorId, string workStationId)
        {
            var data = await telemetryFetchService.GetTelemetryDataInCurrentShiftAsync(operatorId, workStationId);
            return Results.Ok(data);
        }

        public async static Task<IResult> GetTelemetryCountInCurrentShift(ITelemetryFetchService telemetryFetchService, string operatorId, string workStationId)
        {
            var count = await telemetryFetchService.GetCountInCurrentShiftAsync(operatorId, workStationId);
            return Results.Ok(count);
        }



    }
}
