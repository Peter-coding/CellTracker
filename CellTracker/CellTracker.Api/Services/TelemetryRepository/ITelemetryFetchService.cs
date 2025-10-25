using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public interface ITelemetryFetchService
    {
        public Task<List<TelemetryData>> GetTelemetryBetweenAsync(DateTime from, DateTime to);
        public Task<List<TelemetryData>> GetTelemetryDataInCurrentShiftAsync(string OperatorId, string WorkStationId);
        public Task<int> GetTelemetryDataCountInCurrentShiftAsync(string OperatorId, string WorkStationId);
    }
}
