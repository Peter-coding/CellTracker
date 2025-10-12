using Microsoft.AspNetCore.Identity;

namespace CellTracker.Api.Auth
{
    public sealed class RefreshToken
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required DateTime ExpiresAtUtc { get; set; }

        public SiteUser User { get; set; }
    }
}
