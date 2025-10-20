using CellTracker.Api.Configuration.Redis;
using CellTracker.Api.Ingestion.Queue;
using CellTracker.Api.Services.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CellTracker.Api.Infrastructure.Distributor
{
    public class TelemetryDistributorService : BackgroundService, ITelemetryDistributorService
    {
        private readonly IHubContext<SignalRHub> _hub;
        private readonly IRedisQueueService _redisQueueService;

        private readonly string _distributionQueueKey;

        public TelemetryDistributorService(
            IHubContext<SignalRHub> hub,
            IRedisQueueService redisQueueService)
        {
            _hub = hub;
            _redisQueueService = redisQueueService;

            _distributionQueueKey = RedisExtension.GetDistributionQueueKey();
        }

        public async Task SendAsync(string method)
        {
            await _hub.Clients.All.SendAsync(method);
        }

        public async Task SendObjectAsync(string method, object obj)
        {
            await _hub.Clients.All.SendAsync(method, obj);
        }

        public async Task SendGroupAsync(string group, string method, object? obj)
        {
            await _hub.Clients.Group(group).SendAsync(method, obj);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var telemetryData = await _redisQueueService.DequeueAsync(_distributionQueueKey, stoppingToken);

                    if (telemetryData == null)
                    {
                        // There's no item in the queue, so continue
                        continue;
                    }

                    // edit groupName for testing
                    await SendGroupAsync("loginGroup", "Message", telemetryData);
                }
                catch (Exception ex)
                {
                    // Exception and handling
                    Console.WriteLine("Error processing telemetry");
                }
            }
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
