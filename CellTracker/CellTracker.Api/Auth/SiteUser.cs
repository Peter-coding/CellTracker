using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CellTracker.Api.Auth
{
    public class SiteUser : IdentityUser
    {
        [StringLength(75)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(75)]
        [Required]
        public string LastName { get; set; }

        public string? LoginCode { get; set; }
    }
}
