using CellTracker.Api.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models
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

        public ProductionLine ProductionLine { get; set; }

        public ICollection<WorkStation> WorkStations { get; set; } = new List<WorkStation>();
    }
}
