using CellTracker.Api.Ingestion.Model;
using StackExchange.Redis;
using System.Text.Json;

namespace CellTracker.Api.Ingestion.Queue
{
    public class RedisQueueService : IRedisQueueService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _queueKey = "telemetry:queue";

        public RedisQueueService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public async Task EnqueueAsync(TelemetryData data, CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var json = JsonSerializer.Serialize(data);
            await db.ListLeftPushAsync(_queueKey, json);
        }
        public async Task<TelemetryData> DequeueAsync(CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var result = await db.ListRightPopAsync(_queueKey);
            if (result.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<TelemetryData>(result!)!;
        }
    }
}
