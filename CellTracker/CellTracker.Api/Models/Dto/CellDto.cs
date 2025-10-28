using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Dto
{
    public class CellDto
    {

        [MaxLength(35)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }  // optional Description,


    }
}
