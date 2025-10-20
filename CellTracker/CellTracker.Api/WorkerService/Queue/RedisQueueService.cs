using CellTracker.Api.Ingestion.Model;
using StackExchange.Redis;
using System.Text.Json;

namespace CellTracker.Api.Ingestion.Queue
{
    public class RedisQueueService : IRedisQueueService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisQueueService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public async Task EnqueueAsync(string queueKey, TelemetryData data, CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var json = JsonSerializer.Serialize(data);
            await db.ListLeftPushAsync(queueKey, json);
        }
        public async Task<TelemetryData> DequeueAsync(string queueKey, CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var result = await db.ListRightPopAsync(queueKey);
            if (result.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<TelemetryData>(result!)!;
        }
    }
}
