using System.Security.Claims;

namespace CellTracker.Api.Infrastructure.UserIdentiy
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return _httpContextAccessor.HttpContext?.User
                ?? new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new[] {
                            new Claim(ClaimTypes.NameIdentifier, "system")
                }));
        }

        public string GetUsername()
        {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";
            return username;
        }
    }
}
