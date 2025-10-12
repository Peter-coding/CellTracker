using CellTracker.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Configuration.Extension
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            using AppIdentityDbContext appIdentityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            appDbContext.Database.Migrate();
            appIdentityDbContext.Database.Migrate();
        }
    }
}
