using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.Simulation;

namespace CellTracker.Api.Services.Simulation
{
    public interface ISimulationService
    {
        public Task<IEnumerable<SimulationModel>> GetAllSimulations();
        public Task<IResult> StartSimulation(CreateSimulationDto parameters);
        public Task<IResult> StopSimulation(SimulationModel parameters);
        public Task<SimulationModel> AddSimulation(CreateSimulationDto simulationDto);
        public Task<SimulationModel> GetSimulationById(Guid id);
        public  Task<bool> RemoveSimulationById(Guid id);
        public Task<SimulationModel> UpdateSimulation(UpdateSimulationDto updateSimulationDto);

    }
}
