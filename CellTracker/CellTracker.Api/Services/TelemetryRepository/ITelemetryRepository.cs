using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public interface ITelemetryRepository
    {
        public void SaveTelemetryAsync(TelemetryData telemetryData);
        public Task<List<TelemetryData>> GetTelemetryAsync(string query);
    }
}
