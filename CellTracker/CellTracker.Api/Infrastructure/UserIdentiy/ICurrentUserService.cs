using System.Security.Claims;

namespace CellTracker.Api.Infrastructure.UserIdentiy
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal GetUser();
        string GetUsername();
    }
}
