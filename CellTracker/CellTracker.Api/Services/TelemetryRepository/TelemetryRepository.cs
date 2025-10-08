using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Ingestion.Model;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private readonly InfluxDBClient _influxDBClient;
        public TelemetryRepository()
        {
            var connectionString = ConnectionConfiguration.GetInfluxDbConnectionString();
            _influxDBClient = new InfluxDBClient(connectionString);
        }

        public Task<List<TelemetryData>> GetTelemetryAsync(string query)
        {
            return _influxDBClient.GetQueryApi().QueryAsync<TelemetryData>(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }

        public async void SaveTelemetryAsync(TelemetryData telemetryData)
        {
           await _influxDBClient.GetWriteApiAsync().WriteMeasurementAsync(telemetryData, WritePrecision.Ms,
               Environment.GetEnvironmentVariable("INFLUXDB_BUCKET"), Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }
    }
}
