using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Dto
{
    public class WorkStationDto
    {
        public string MqttDeviceId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }  // optional Description,
        public Guid CellId { get; set; }
        [Required]
        public Guid ProductionLineId { get; set; }
    }
}
