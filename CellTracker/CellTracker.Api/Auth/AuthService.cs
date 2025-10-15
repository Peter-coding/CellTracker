using CellTracker.Api.Auth.DTOs;
using CellTracker.Api.Configuration.JwtSettings;
using CellTracker.Api.Data;
using InfluxDB.Client.Api.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CellTracker.Api.Auth
{
    public sealed class AuthService : IAuthService
    {
        UserManager<SiteUser> _userManager;
        AppIdentityDbContext _appIdentityDbContext;
        TokenProvider _tokenProvider;
        IOptions<JwtAuthOptions> _options;
        private readonly JwtAuthOptions _jwtAuthOptions;

        public AuthService(
             UserManager<SiteUser> userManager,
             AppIdentityDbContext appIdentityDbContext,
             TokenProvider tokenProvider,
             IOptions<JwtAuthOptions> options)
        {
            _userManager = userManager;
            _appIdentityDbContext = appIdentityDbContext;
            _tokenProvider = tokenProvider;
            _options = options;

            _jwtAuthOptions = _options.Value;
        }

        public async Task<IResult> Register(RegisterUserDto registerUserDto)
        {

            var identityUser = new SiteUser
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Email = registerUserDto.Email,
                UserName = registerUserDto.Email,
                LoginCode = registerUserDto.LoginCode,
            };

            IdentityResult createUserResult = await _userManager.CreateAsync(identityUser, registerUserDto.Password);

            if (!createUserResult.Succeeded)
            {
                var extensions = new Dictionary<string, object?>
                {
                    {
                        "errors",
                        createUserResult.Errors.ToDictionary(e => e.Code, e => e.Description)
                    }
                };
                return Results.Problem(
                    detail: "Unable to register user, please try again",
                    statusCode: StatusCodes.Status400BadRequest,
                    extensions: extensions);
            }
            IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(identityUser, registerUserDto.Role);

            if (!addToRoleResult.Succeeded)
            {
                var extensions = new Dictionary<string, object?>
                {
                    {
                        "errors",
                        addToRoleResult.Errors.ToDictionary(e => e.Code, e => e.Description)
                    }
                };
                return Results.Problem(
                    detail: "Unable to register user, please try again",
                    statusCode: StatusCodes.Status400BadRequest,
                    extensions: extensions);
            }

            var tokenRequest = new TokenRequest(identityUser.Id, identityUser.Email, [registerUserDto.Role]);
            AccessTokensDto accessTokens = _tokenProvider.Create(tokenRequest);

            var refreshToken = new RefreshToken
            {
                Id = Guid.CreateVersion7(),
                UserId = identityUser.Id,
                Token = accessTokens.RefreshToken,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationDays)
            };

            _appIdentityDbContext.RefreshTokens.Add(refreshToken);

            await _appIdentityDbContext.SaveChangesAsync();

            return Results.Ok(accessTokens);
        }

        public async Task<IResult> Login(LoginUserDto loginUserDto)
        {
            SiteUser? siteUser = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (siteUser is null || !await _userManager.CheckPasswordAsync(siteUser, loginUserDto.Password))
            {
                return Results.Unauthorized();
            }

            IList<string> roles = await _userManager.GetRolesAsync(siteUser);

            var tokenRequest = new TokenRequest(siteUser.Id, siteUser.Email!, roles);
            AccessTokensDto accessTokens = _tokenProvider.Create(tokenRequest);

            var refreshToken = new RefreshToken
            {
                Id = Guid.CreateVersion7(),
                UserId = siteUser.Id,
                Token = accessTokens.RefreshToken,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationDays)
            };
            _appIdentityDbContext.RefreshTokens.Add(refreshToken);

            await _appIdentityDbContext.SaveChangesAsync();

            return Results.Ok(accessTokens);
        }

        public async Task<IResult> Refresh(RefreshTokenDto refreshTokenDto)
        {
            RefreshToken? refreshToken = await _appIdentityDbContext.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshTokenDto.RefreshToken);

            if (refreshToken is null)
            {
                return Results.Unauthorized();
            }

            if (refreshToken.ExpiresAtUtc < DateTime.UtcNow)
            {
                return Results.Unauthorized();
            }

            IList<string> roles = await _userManager.GetRolesAsync(refreshToken.User);

            var tokenRequest = new TokenRequest(refreshToken.User.Id, refreshToken.User.Email!, roles);
            AccessTokensDto accessTokens = _tokenProvider.Create(tokenRequest);

            refreshToken.Token = accessTokens.RefreshToken;
            refreshToken.ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenExpirationDays);

            await _appIdentityDbContext.SaveChangesAsync();

            return Results.Ok(accessTokens);
        }

        public async Task<IResult> GetUsers()
        {
            var siteUsers = await _userManager.Users.ToListAsync();
            var siteUserDtos = new List<SiteUserDto>();
            foreach (var siteUser in siteUsers)
            {
                siteUserDtos.Add(new SiteUserDto
                {
                    FirstName = siteUser.FirstName,
                    LastName = siteUser.LastName,
                    Email = siteUser.Email,
                    PhoneNumber = siteUser.PhoneNumber,
                    EmailConfirmed = siteUser.EmailConfirmed,
                    LoginCode = siteUser.LoginCode,
                    Role = (await _userManager.GetRolesAsync(siteUser)).FirstOrDefault() ?? "undefined role"
                });
            }
            return Results.Ok(siteUserDtos);
        }
    }
}
