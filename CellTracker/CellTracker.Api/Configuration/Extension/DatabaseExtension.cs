using CellTracker.Api.Auth;
using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CellTracker.Api.Configuration.Extension
{
    public static class DatabaseExtension
    {

        public static IServiceCollection RegisterDbContextExtension(
            this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(ConnectionConfiguration.GetConnectionString(),
                npsqlOptions => npsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Application))
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning))
            );

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseNpgsql(ConnectionConfiguration.GetConnectionString(),
                npsqlOptions => npsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Identity))
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning))
            );

            return services;
        }

        public static async Task SeedInitialDataAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            RoleManager<IdentityRole> roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                if (!await roleManager.RoleExistsAsync(Roles.Operator))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Operator));
                }
                if (!await roleManager.RoleExistsAsync(Roles.Manager))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Manager));
                }
                if (!await roleManager.RoleExistsAsync(Roles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                }

                app.Logger.LogInformation("Successfully created roles.");
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "An error occurred while seeding initial data.");
                throw;
            }
        }
    }
}
