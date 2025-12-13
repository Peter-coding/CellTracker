using CellTracker.Api.Configuration.MqttClient;
using CellTracker.Api.Configuration.Redis;
using CellTracker.Api.ExceptionHandler;
using CellTracker.Api.Infrastructure.Distributor;
using CellTracker.Api.Infrastructure.Logging;
using CellTracker.Api.Infrastructure.UserIdentiy;
using CellTracker.Api.Ingestion.Queue;
using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Models.Simulation;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.CellService;
using CellTracker.Api.Services.FactoryService;
using CellTracker.Api.Services.OperatorTaskService;
using CellTracker.Api.Services.ProductionLineService;
using CellTracker.Api.Services.Simulation;
using CellTracker.Api.Services.TelemetryRepository;
using CellTracker.Api.Services.WorkStationService;

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

            // Add Redis
            builder.Services.AddRedisOptions();
            builder.Services.AddSingleton<IRedisQueueService, RedisQueueService>();

            // Add GlobalExceptionHandler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            //Add ProblemDetails middleware with requestId extension (can be reached in GlobalExceptionHandler)
            builder.Services.AddProblemDetails(configure =>
            {
                configure.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                };
            });

            // Get current user from request
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

            // Create logging service
            builder.Services.AddSingleton<ILoggingService, LoggingService>();

            // Add Mqtt Options to set up Mqtt client
            builder.Services.AddMqttClientOptions(configuration);

            // DbContext Registration
            builder.Services.RegisterDbContextExtension();

            // Add Auth
            builder.AddAuthenticationServices();

            //Add more Repositories later to inject it into UnitOfWork
            builder.Services.AddScoped<IRepository<OperatorTask>, OperatorTaskRepository>();
            builder.Services.AddScoped<IRepository<Factory>, FactoryRepository>();
            builder.Services.AddScoped<IRepository<ProductionLine>, ProductionLineRepository>();
            builder.Services.AddScoped<IRepository<Cell>, CellRepository>();
            builder.Services.AddScoped<IRepository<WorkStation>, WorkStationRepository>();
            builder.Services.AddScoped<IRepository<SimulationModel>, SimulationRepository>();

            // Add service
            builder.Services.AddScoped<IOperatorTaskService, OperatorTaskService>();
            builder.Services.AddScoped<IFactoryService, FactoryService>();
            builder.Services.AddScoped<IProductionLineService, ProductionLineService>();
            builder.Services.AddScoped<ICellService, CellService>();
            builder.Services.AddScoped<IWorkStationService, WorkStationService>();
            builder.Services.AddScoped<ISimulationService, SimulationService>();
            builder.Services.AddSingleton<ISimulationStatusManager, SimulationStatusManager>();

            // Add UnitOfWork pattern
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<ITelemetryFetchService, TelemetryFetchService>();
            // Add SignalR
            builder.Services.AddSignalR();
            builder.Services.AddHostedService<TelemetryDistributorService>();



            return builder;
        }
    }
}
