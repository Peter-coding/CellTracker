namespace CellTracker.Api.Auth.DTOs
{
    public sealed record TokenRequest(string UserId, string Email, IEnumerable<string> Roles);
}
