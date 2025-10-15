namespace CellTracker.Api.Auth.DTOs
{
    public sealed record RefreshTokenDto
    {
        public required string RefreshToken { get; init; }
    }
}
