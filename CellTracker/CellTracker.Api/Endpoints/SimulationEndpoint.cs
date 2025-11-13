using CellTracker.Api.Models.Simulation;
using CellTracker.Api.Services.Simulation;

namespace CellTracker.Api.Endpoints
{
    public static class SimulationEndpoint
    {
        public static void MapSimulationEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";

            app.MapPost($"{path}/StartSimulation", StartSimulation);
        }

        public async static void StartSimulation(ISimulationService simulationService, SimulationParameters parameters)
        {
            await simulationService.StartSimulation(parameters);
        }
    }
}
