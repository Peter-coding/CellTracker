using CellTracker.Api.Models.Simulation;

namespace CellTracker.Api.Models.Dto
{
    public class UpdateSimulationDto
    {
        public Guid Id { get; set; }
        public Shift Shift { get; set; }
        public int NumberOfProductsMade { get; set; }
        public int MinutesOfSimulation { get; set; }
        public Guid ProductionLineId { get; set; }
    }
}
