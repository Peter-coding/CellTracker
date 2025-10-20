using CellTracker.Api.Ingestion.Model;

namespace CellTracker.Api.Ingestion.Queue
{
    public interface IRedisQueueService
    {
        Task EnqueueAsync(string queueKey, TelemetryData data, CancellationToken ct);
        Task<TelemetryData> DequeueAsync(string queueKey, CancellationToken ct);
    }
}
