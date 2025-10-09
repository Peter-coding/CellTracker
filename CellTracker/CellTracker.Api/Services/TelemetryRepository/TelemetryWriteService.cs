using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Ingestion.Model;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public class TelemetryWriteService : ITelemetryWriteService
    {
        private readonly InfluxDBClient _influxDBClient;
        public TelemetryWriteService()
        {
            var connectionString = ConnectionConfiguration.GetInfluxDbConnectionString();
            _influxDBClient = new InfluxDBClient(connectionString);
        }

        public async void SaveTelemetryAsync(TelemetryData telemetryData)
        {
           await _influxDBClient.GetWriteApiAsync().WriteMeasurementAsync(telemetryData, WritePrecision.Ms,
               Environment.GetEnvironmentVariable("INFLUXDB_BUCKET"), Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }
    }
}
