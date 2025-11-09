using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool? IsDeleted { get; set; } = false;

    }
}
