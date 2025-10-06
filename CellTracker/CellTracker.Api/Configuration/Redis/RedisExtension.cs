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
    }
}
