using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.WorkerService.Validator
{
    public interface ITelemetryValidatorService
    {
        Task<bool> ValidateAsync(TelemetryData telemetry);
    }
}
