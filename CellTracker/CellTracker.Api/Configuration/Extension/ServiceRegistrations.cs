using CellTracker.Api.Infrastructure.Logging;
using CellTracker.Api.Infrastructure.UserIdentiy;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.Operator;
using CellTracker.Api.Services.TelemetryRepository;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Configuration.Extension
{
    public static class ServiceRegistrations
    {
        public static WebApplicationBuilder RegisterServicesExtension(
            this WebApplicationBuilder builder,
            IConfiguration configuration)
        {
            // Adding Logging
            builder.Logging.ClearProviders();
            builder.Services.AddConfigureSerilog();

            // Get current user from request
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

            // Create logging service
            builder.Services.AddSingleton<ILoggingService, LoggingService>();

            // DbContext Registration
            builder.Services.RegisterDbContextExtension();

            // Add Auth
            builder.AddAuthenticationServices();

            //Add more Repositories later to inject it into UnitOfWork
            builder.Services.AddScoped<IRepository<OperatorTask>, OperatorTaskRepository>();

            // Add service
            builder.Services.AddScoped<IOperatorService, OperatorService>();

            // Add UnitOfWork pattern
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITelemetryFetchService, TelemetryFetchService>();

            // Add SignalR
            builder.Services.AddSignalR();

            return builder;
        }
    }
}
