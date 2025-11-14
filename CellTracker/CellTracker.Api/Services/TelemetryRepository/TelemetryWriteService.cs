using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Configuration.Redis;
using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Ingestion.Queue;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public class TelemetryWriteService : ITelemetryWriteService
    {
        private readonly InfluxDBClient _influxDBClient;
        private readonly IRedisQueueService _redisQueueService;

        private readonly string _validatedQueueKey;

        public TelemetryWriteService(IRedisQueueService redisQueueService)
        {
            var connectionString = ConnectionConfiguration.GetInfluxDbConnectionString();
            _influxDBClient = new InfluxDBClient(connectionString);
            _redisQueueService = redisQueueService;

            _validatedQueueKey = RedisExtension.GetValidatedQueueKey();
        }

        public async void SaveTelemetryAsync(TelemetryData telemetryData)
        {
           await _influxDBClient.GetWriteApiAsync().WriteMeasurementAsync(telemetryData, WritePrecision.Ms,
               Environment.GetEnvironmentVariable("INFLUXDB_BUCKET"), Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }

        public async void SaveTelemetryBatchAsync(CancellationToken cancellationToken)
        {
            List<TelemetryData> validatedTelemetryData = [];
      
            while (true)
            {
                var data = await _redisQueueService.DequeueAsync(_validatedQueueKey, cancellationToken);
                if (data == null) break;
                validatedTelemetryData.Add(data);
            }

            await _influxDBClient.GetWriteApiAsync().WriteMeasurementsAsync(validatedTelemetryData, WritePrecision.Ms,
                Environment.GetEnvironmentVariable("INFLUXDB_BUCKET"), Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }

        public async Task<IResult> DeleteAllTelemetryData()
        {
            //delete all telemetry data
            var deleteApi = _influxDBClient.GetDeleteApi();
            var start = DateTime.UtcNow.AddYears(-10);
            var stop = DateTime.UtcNow;
            await deleteApi.Delete(start, stop,
                "_measurement=\"Telemetry\"",
                Environment.GetEnvironmentVariable("INFLUXDB_BUCKET"),
                Environment.GetEnvironmentVariable("INFLUXDB_ORG"));

            return Results.Ok();
        }
    }
}
