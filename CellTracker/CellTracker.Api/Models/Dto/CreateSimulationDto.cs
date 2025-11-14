using CellTracker.Api.Models.Simulation;

namespace CellTracker.Api.Models.Dto
{
    public class CreateSimulationDto
    {
        public Shift Shift { get; set; }
        public int NumberOfProductsMade { get; set; }
        public int MinutesOfSimulation { get; set; }
        public Guid ProductionLineId { get; set; }
    }
}
