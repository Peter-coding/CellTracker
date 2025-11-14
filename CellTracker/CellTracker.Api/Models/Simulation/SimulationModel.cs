using CellTracker.Api.Models.Base;

namespace CellTracker.Api.Models.Simulation
{
    public class SimulationModel : BaseEntity
    {
        public Shift Shift { get; set; }
        public int NumberOfProductsMade { get; set; }
        public int MinutesOfSimulation { get; set; }
        public Guid ProductionLineId { get; set; }
    }

    public enum Shift
    {
        Morning, Afternoon, Night
    }
}
