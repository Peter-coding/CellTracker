using CellTracker.Api.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Configuration
{
    public class WorkStation : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(35)]
        public string Name { get; set; }
        public short OrdinalNumber { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }  // optional Description

        [Required]
        public Guid CellId { get; set; }
        public Cell Cell { get; set; }
    }
}
