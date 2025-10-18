using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.Ingestion.Queue
{
    public interface IRedisQueueService
    {
        Task EnqueueAsync(TelemetryData data, CancellationToken ct);
        Task<TelemetryData> DequeueAsync(CancellationToken ct);
        Task EnqueValidatedAsync(TelemetryData data, CancellationToken ct);
        Task<TelemetryData> DequeueValidatedAsync(CancellationToken ct);
    }
}
