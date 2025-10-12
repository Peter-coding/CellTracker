using CellTracker.Api.Auth.DTOs;

namespace CellTracker.Api.Auth
{
    public interface IAuthService
    {
        Task<IResult> Register(RegisterUserDto registerUserDto);
        Task<IResult> Login(LoginUserDto loginUserDto);
        Task<IResult> Refresh(RefreshTokenDto refreshTokenDto);
        Task<IResult> GetUsers();
    }
}
