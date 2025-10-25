using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Services.Operator;
using CellTracker.Api.Services.TelemetryRepository;

namespace CellTracker.Api.Endpoints
{
    public static class TelemetryEndpoint
    {
        public static void MapTelemetryEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";
     
            app.MapGet($"{path}/GetTelemetryData", GetTelemetryData);
        }
        public async static Task<IResult> GetTelemetryData(ITelemetryFetchService telemetryFetchService, DateTime from, DateTime to)
        {
            if (from > to)
            {
                var tmp = to;
                to = from;
                from = tmp;
            }
            var data = await telemetryFetchService.GetTelemetryBetweenAsync(from, to);
            return Results.Ok(data);
        }

    }
}
