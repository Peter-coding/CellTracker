using CellTracker.Api.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CellTracker.Api.Models.Configuration
{
    public class Cell : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(35)]
        public string Name { get; set; }

        public int OrdinalNumber { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }  // optional Description,

        [Required]
        public Guid ProductionLineId { get; set; }

        [JsonIgnore]
        public ProductionLine ProductionLine { get; set; }
        [JsonIgnore]
        public ICollection<WorkStation> WorkStations { get; set; } = new List<WorkStation>();
    }
}
