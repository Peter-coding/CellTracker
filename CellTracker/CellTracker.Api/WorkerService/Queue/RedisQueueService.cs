using CellTracker.Api.Ingestion.Model;
using StackExchange.Redis;
using System.Text.Json;

namespace CellTracker.Api.Ingestion.Queue
{
    public class RedisQueueService : IRedisQueueService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _rawQueueKey = "telemetry:raw_queue";
        private readonly string _validatedQueueKey = "telemetry:validated_queue";

        public RedisQueueService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public async Task EnqueueAsync(TelemetryData data, CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var json = JsonSerializer.Serialize(data);
            await db.ListLeftPushAsync(_rawQueueKey, json);
        }
        public async Task<TelemetryData> DequeueAsync(CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var result = await db.ListRightPopAsync(_rawQueueKey);
            if (result.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<TelemetryData>(result!)!;
        }

        public async Task EnqueValidatedAsync(TelemetryData data, CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var json = JsonSerializer.Serialize(data);
            await db.ListLeftPushAsync(_validatedQueueKey, json);
        }

        public async Task<TelemetryData> DequeueValidatedAsync(CancellationToken ct)
        {
            var db = _redis.GetDatabase();
            var result = await db.ListRightPopAsync(_validatedQueueKey);
            if (result.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<TelemetryData>(result!)!;
        }
    }
}
