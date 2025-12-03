using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public interface ITelemetryFetchService
    {
        public Task<List<TelemetryData>> GetBetweenAsync(DateTime from, DateTime to);
        public Task<List<TelemetryData>> GetTelemetryOfWsCurrentShiftAsync(Guid wsId, DateTime currentTime);
        public Task<List<TelemetryData>> GetTelemetryDataInCurrentShiftAsync(string OperatorId, string WorkStationId);
        public Task<int> GetCountInCurrentShiftAsync(string OperatorId, string WorkStationId);
       // public Task<Dictionary<string, int>> GetTelemetryCountPerWorkStationInCurrentShiftAsync(Guid cellId);
        public Task<Dictionary<string, int>> GetTelemetryCountPerProductionLineAsync(Guid productionLineId);
        public Task<List<TelemetryData>> GetTelemetryDataInCurrentShiftOfWorkStationAsync(Guid wsId, DateTime currentTime);
        public DateTime GetCurrentShiftStart(DateTime currentTime);
    }
}
