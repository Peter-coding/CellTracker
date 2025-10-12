namespace CellTracker.Api.Auth.DTOs
{
    public sealed record RegisterUserDto
    {
        public required string Email { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Password { get; init; }
        public required string ConfirmPassword { get; init; }
        public string LoginCode { get; init; }
        public required string Role { get; init; }

    }
}
