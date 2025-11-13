using CellTracker.Api.Models.Simulation;

namespace CellTracker.Api.Services.Simulation
{
    public interface ISimulationService
    {
        public Task<IResult> StartSimulation(SimulationParameters parameters);
    }
}
