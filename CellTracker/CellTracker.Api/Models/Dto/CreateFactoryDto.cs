using System.ComponentModel.DataAnnotations;

namespace CellTracker.Api.Models.Dto
{
    public class CreateFactoryDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
