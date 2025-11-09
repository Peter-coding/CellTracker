using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Dto
{
    public class UpdateWorkStationDto
    {
        public Guid Id { get; set; }
        public string MqttDeviceId { get; set; }
        public string Name { get; set; }
        public int OrdinalNumber { get; set; }
        public string? Description { get; set; }
        public Guid CellId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
