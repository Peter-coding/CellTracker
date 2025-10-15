namespace CellTracker.Api.Auth.DTOs
{
    public sealed record LoginUserDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string LoginCode { get; set; }
    }
}
