using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.WorkerService.Validator
{
    public class TelemetryValidatorService : ITelemetryValidatorService
    {
        private readonly ILogger<TelemetryValidatorService> _logger;

        public TelemetryValidatorService(ILogger<TelemetryValidatorService> logger)
        {
            _logger = logger;
        }

        public Task<bool> ValidateAsync(TelemetryData telemetry)
        {
            // Todo message validation
            return Task.FromResult(true);
        }
    }
}
