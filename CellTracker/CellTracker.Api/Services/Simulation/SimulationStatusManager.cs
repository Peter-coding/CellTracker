using CellTracker.Api.Models.Simulation;

namespace CellTracker.Api.Services.Simulation
{
    public class SimulationStatusManager : ISimulationStatusManager
    {
        private Dictionary<Guid, SimulationStatus> simulationStatus = [];

        public void SetSimulationStatus(Guid simulationId, SimulationStatus status)
        {
            if (!simulationStatus.ContainsKey(simulationId))
            {
                simulationStatus.Add(simulationId, status);
            }
            else
            {
                simulationStatus[simulationId] = status;
            }
        }

        public SimulationStatus GetSimulationStatus(Guid simulationId)
        {
            if (simulationStatus.ContainsKey(simulationId))
            {
                return simulationStatus[simulationId];
            }
            return SimulationStatus.New;
        }
    }
}
