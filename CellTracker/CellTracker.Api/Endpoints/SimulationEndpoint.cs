using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.Simulation;
using CellTracker.Api.Services.Simulation;

namespace CellTracker.Api.Endpoints
{
    public static class SimulationEndpoint
    {
        public static void MapSimulationEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";

            app.MapGet($"{path}/Start/{{id}}", StartSimulationAsync);
            app.MapGet($"{path}/Stop/{{id}}", StopSimulationAsync);
            app.MapGet($"{path}/Continue/{{id}}", ContinueSimulationAsync);
            app.MapGet($"{path}/GetAll", GetAllSimulationsAsync);
            app.MapGet($"{path}/Get/{{id}}", GetSimulationByIdAsync);
            app.MapPost($"{path}/Add", AddSimulationAsync);
            app.MapDelete($"{path}/Delete/{{id}}", DeleteSimulationByIdAsync);
            app.MapPut($"{path}/Update", UpdateSimulationAsync);
        }

        public async static Task<IResult> StartSimulationAsync(ISimulationService simulationService, Guid id)
        {
            await simulationService.StartSimulation(id);
            return Results.Ok();
        }

        public async static void StopSimulationAsync(ISimulationService simulationService, Guid id)
        {
            await simulationService.StopSimulation(id);
        }

        public async static void ContinueSimulationAsync(ISimulationService simulationService, Guid id)
        {
            await simulationService.ContinueSimulation(id);
        }

        public async static Task<IResult> GetAllSimulationsAsync(ISimulationService simulationService)
        {
            var result = await simulationService.GetAllSimulations();
            if (result == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(result);
        }

        public async static Task<IResult> GetSimulationByIdAsync(ISimulationService simulationService, Guid id)
        {
            var result = await simulationService.GetSimulationById(id);
            if (result == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(result);
        }

        public async static Task<IResult> AddSimulationAsync(ISimulationService simulationService, CreateSimulationDto simulationDto)
        {
            var result = await simulationService.AddSimulation(simulationDto);
            if (result == null)
            {
                return Results.BadRequest("Simulation line could not be created");
            }
            return Results.Ok(result);
        }

        public async static Task<IResult> DeleteSimulationByIdAsync(ISimulationService simulationService, Guid id)
        {
            var result = await simulationService.RemoveSimulationById(id);
            if (!result)
            {
                return Results.NotFound();
            }
            return Results.Ok();
        }

        public async static Task<IResult> UpdateSimulationAsync(ISimulationService simulationService, UpdateSimulationDto updateSimulationDto)
        {
            var result = await simulationService.UpdateSimulation(updateSimulationDto);
            if (result == null)
            {
                return Results.BadRequest("Simulation could not be updated");
            }
            return Results.Ok(result);
        }
    }
}
