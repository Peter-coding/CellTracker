using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public interface ITelemetryWriteService
    {
        public void SaveTelemetryAsync(TelemetryData telemetryData);
    }
}
