using CellTracker.Api.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CellTracker.Api.Models.Configuration
{
    public class Factory : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
        public ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();
    }
}
