using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.Simulation;

namespace CellTracker.Api.Services.Simulation
{
    public interface ISimulationService
    {
        public Task<IEnumerable<SimulationModel>> GetAllSimulations();
        public Task<IResult> StartSimulation(Guid simulationId);
        public Task<IResult> StopSimulation(Guid simulationdId);
        public Task<IResult> ContinueSimulation(Guid simulationdId);
        public Task<SimulationModel> AddSimulation(CreateSimulationDto simulationDto);
        public Task<SimulationModel> GetSimulationById(Guid id);
        public  Task<bool> RemoveSimulationById(Guid id);
        public Task<SimulationModel> UpdateSimulation(UpdateSimulationDto updateSimulationDto);

    }
}
