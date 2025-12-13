namespace CellTracker.Api.Services.Simulation
{
    public interface ISimulationStatusManager
    {
        public void SetSimulationStatus(Guid simulationId, SimulationStatus status);
        public SimulationStatus GetSimulationStatus(Guid simulationId);
    }

    public enum SimulationStatus
    {
        New,
        Running,
        Stopped,
        Completed,
        Finished
    }
}
