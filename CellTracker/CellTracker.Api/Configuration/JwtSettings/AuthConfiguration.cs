using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CellTracker.Api.Configuration.JwtSettings
{
    public static class AuthConfiguration
    {
        public static JwtAuthOptions GetJwtSettings()
        {
            return new JwtAuthOptions
            {
                Key = Environment.GetEnvironmentVariable("JWT__KEY")
                  ?? throw new InvalidOperationException("Missing JWT__KEY"),
                Issuer = Environment.GetEnvironmentVariable("JWT__ISSUER")
                  ?? "celltracker.api",
                Audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")
                  ?? "celltracker.app",
                ExpirationInMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT__EXPIRATIONINMINUTES"), out var exp)
                ? exp : 30,
                RefreshTokenExpirationDays = int.TryParse(Environment.GetEnvironmentVariable("JWT__REFRESHTOKENEXPIRATIONDAYS"), out var refreshExp)
                ? refreshExp : 7
            };
        }
    }
}
