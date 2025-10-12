
namespace CellTracker.Api.Auth.DTOs
{
    public record SiteUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? LoginCode { get; set; }
        public string Role { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
