using CellTracker.Api.Auth;
using CellTracker.Api.Auth.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CellTracker.Api.Endpoints
{
    public static class AuthEndpoint
    {
        public static void MapAuthEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            app.MapPost("/Authentication/Login", Login);
            //app.MapPost("/Authentication/LoginWithCode", LoginWithCode);
            app.MapPost("/Authentication/Register", Register);
            app.MapPost("/Authentication/Refresh", Refresh);
            //app.MapGet("/Authentication/GetActiveUsers", GetActiveUsers);
            app.MapGet("/Authentication/GetUsers", GetUsers);
        }

        public static async Task<IResult> Register(
            UserManager<SiteUser> userManager, 
            RegisterUserDto registerUserDto,
            IAuthService authService)
        {
            return await authService.Register(registerUserDto);
        }

        public static async Task<IResult> Login(
            UserManager<SiteUser> userManager,
            LoginUserDto loginUserDto,
            IAuthService authService)
        {
            return await authService.Login(loginUserDto);
        }
        public static async Task<IResult> Refresh(
            UserManager<SiteUser> userManager,
            RefreshTokenDto refreshTokenDto,
            IAuthService authService)
        {
            return await authService.Refresh(refreshTokenDto);
        }
        public static async Task<IResult> GetUsers(
            UserManager<SiteUser> userManager,
            IAuthService authService)
        {
            return await authService.GetUsers();
        }
    }
}
