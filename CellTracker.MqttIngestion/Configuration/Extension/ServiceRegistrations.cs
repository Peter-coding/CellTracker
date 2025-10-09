using CellTracker.Api.Ingestion.Queue;
using CellTracker.Api.Services.TestMqtt;
using CellTracker.Api.WorkerService.Ingestion;
using CellTracker.Api.WorkerService.Processor;
using CellTracker.Api.WorkerService.Validator;
using CellTracker.Api.Configuration.Redis;
using CellTracker.Api.Configuration.MqttClient;
using CellTracker.Api.Services.TelemetryRepository;


namespace CellTracker.MqttIngestion.Configuration.Extension
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection RegisterWorkerServicesExtension(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add validator service
            services.AddSingleton<ITelemetryValidatorService, TelemetryValidatorService>();

            // Add Redis
            services.AddRedisOptions();
            services.AddSingleton<IRedisQueueService, RedisQueueService>();

            // Add Mqtt Options to set up Mqtt client
            services.AddMqttClientOptions(configuration);

            services.AddScoped<ITelemetryWriteService, TelemetryWriteService>();

            // Add Hosted Services
            services.RegisterHostedServicesExtension();

            return services;
        }

        private static IServiceCollection RegisterHostedServicesExtension
            (this IServiceCollection services)
        {
            // Test BackgroundService which sends telemetry data for Broker for testing purposes
            services.AddHostedService<TestMqttService>();
            // MqttClient which ingests the data
            services.AddHostedService<TelemetryIngestionService>();
            // Telemetry processor service
            services.AddHostedService<TelemetryProcessorService>();

            return services;
        }
    }
}
