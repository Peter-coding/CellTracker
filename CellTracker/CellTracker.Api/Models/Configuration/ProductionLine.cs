using CellTracker.Api.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CellTracker.Api.Models.Configuration
{
    public class ProductionLine : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(35)]
        public string Name { get; set; }

        public short OrdinalNumber { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }  // optional Description

        public ProductionLineStatus Status { get; set; }

        [Required]
        public Guid FactoryId { get; set; }

        [JsonIgnore]
        public Factory Factory { get; set; }

        [JsonIgnore]
        public ICollection<Cell> Cells { get; set; } = new List<Cell>();
    }

    public enum ProductionLineStatus
    {
        Running,
        Stopped,
        Maintenance,
        Error
    }
}

