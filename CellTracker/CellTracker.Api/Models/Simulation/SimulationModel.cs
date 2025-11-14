using CellTracker.Api.Models.Base;
using CellTracker.Api.Models.Configuration;
using System.Text.Json.Serialization;

namespace CellTracker.Api.Models.Simulation
{
    public class SimulationModel : BaseEntity
    {
        public Shift Shift { get; set; }
        public int NumberOfProductsMade { get; set; }
        public int MinutesOfSimulation { get; set; }
        public Guid ProductionLineId { get; set; }
        [JsonIgnore]
        public ProductionLine ProductionLine { get; set; }
    }

    public enum Shift
    {
        Morning, Afternoon, Night
    }
}
