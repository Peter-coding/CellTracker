using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Ingestion.Model;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

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
        public Task<List<TelemetryData>> GetTelemetryAsync(string workStationId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public void SaveTelemetryAsync(TelemetryData telemetryData)
        {
            _influxDBClient.GetWriteApiAsync().WriteMeasurementAsync(telemetryData, WritePrecision.Ms, "CellTracker", "CellTracker" );
        }
    }
}
