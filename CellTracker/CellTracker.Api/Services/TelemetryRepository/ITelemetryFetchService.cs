using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public interface ITelemetryFetchService
    {
        public Task<List<TelemetryData>> GetTelemetryAsync(DateTime from, DateTime to);
    }
}
