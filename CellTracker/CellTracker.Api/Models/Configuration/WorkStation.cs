using CellTracker.Api.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CellTracker.Api.Models.Configuration
{
    public class WorkStation : BaseEntity
    {
        [Required]
        public string MqttDeviceId { get; set; }

        [MaxLength(35)]
        public string Name { get; set; }
        public int OrdinalNumber { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }  // optional Description

        [Required]
        public Guid CellId { get; set; }

        [JsonIgnore]
        public Cell Cell { get; set; }
    }
}
