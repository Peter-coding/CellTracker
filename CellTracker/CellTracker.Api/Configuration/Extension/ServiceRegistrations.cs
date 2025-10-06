using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Data;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.Operator;
using CellTracker.Api.Services.TelemetryRepository;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Configuration.Extension
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection RegisterServicesExtension(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext Registration
            services.RegisterDbContextExtension();

            //Add more Repositories later to inject it into UnitOfWork
            services.AddScoped<IRepository<OperatorTask>, OperatorTaskRepository>();

            // Add service
            services.AddScoped<IOperatorService, OperatorService>();

            // Add UnitOfWork pattern
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add TelemetryReposiroty
            services.AddScoped<ITelemetryRepository, TelemetryRepository>();


            return services;
        }

        private static IServiceCollection RegisterDbContextExtension(
            this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(ConnectionConfiguration.GetConnectionString())
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning))
            );
            return services;
        }
    }
}
