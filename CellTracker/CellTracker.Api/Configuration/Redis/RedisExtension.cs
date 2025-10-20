using StackExchange.Redis;

namespace CellTracker.Api.Configuration.Redis
{
    public static class RedisExtension
    {
        public static IServiceCollection AddRedisOptions(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect("redis:6379"));

            return services;
        }

        public static string GetRawQueueKey()
        {
            var queueKey = Environment.GetEnvironmentVariable("RAW_QK");
            return queueKey;
        }
        public static string GetValidatedQueueKey()
        {
            var queueKey = Environment.GetEnvironmentVariable("VALID_QK");
            return queueKey;
        }
        public static string GetDistributionQueueKey()
        {
            var queueKey = Environment.GetEnvironmentVariable("DISTR_QK");
            return queueKey;
        }
    }
}
