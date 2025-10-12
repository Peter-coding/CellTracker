using CellTracker.Api.Auth;
using CellTracker.Api.Configuration.JwtSettings;
using CellTracker.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CellTracker.Api.Configuration.Extension
{
    public static class AuthExtension
    {

        public static WebApplicationBuilder AddAuthenticationServices(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization();
            builder.Services
                .AddIdentity<SiteUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.AddJwtServices();

            builder.Services.AddTransient<TokenProvider>();


            return builder;
        }

        public static WebApplicationBuilder AddJwtServices(
            this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtAuthOptions>(builder.Configuration.GetSection("Jwt"));

            JwtAuthOptions jwtAuthOptions = AuthConfiguration.GetJwtSettings()!;

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtAuthOptions.Issuer,
                        ValidAudience = jwtAuthOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthOptions.Key))
                    };
                });

            return builder;
        }
    }
}
