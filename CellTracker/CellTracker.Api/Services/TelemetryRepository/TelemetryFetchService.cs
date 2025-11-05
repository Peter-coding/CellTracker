using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Ingestion.Model;
using InfluxDB.Client;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public class TelemetryFetchService : ITelemetryFetchService
    {
        private readonly InfluxDBClient _influxDBClient;
        public TelemetryFetchService()
        {
            var connectionString = ConnectionConfiguration.GetInfluxDbConnectionString();
            _influxDBClient = new InfluxDBClient(connectionString);
        }

        public Task<List<TelemetryData>> GetTelemetryAsync(DateTime from, DateTime to)
        {
            var fromUtc = from.ToUniversalTime();
            var toUtc = to.ToUniversalTime();

            string query = $@"
                from(bucket: ""CellTracker"")
                   |> range(start: {fromUtc:yyyy-MM-ddTHH:mm:ssZ}, stop: {toUtc:yyyy-MM-ddTHH:mm:ssZ})
                   |> filter(fn: (r) => r._measurement == ""Telemetry"")
                   |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                 ";

            return _influxDBClient.GetQueryApi().QueryAsync<TelemetryData>(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }
    }
}
