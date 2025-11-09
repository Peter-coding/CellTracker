using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Dto
{
    public class CellDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid ProductionLineId { get; set; }
    }
}
