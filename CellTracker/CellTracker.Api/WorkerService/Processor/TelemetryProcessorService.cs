using CellTracker.Api.Configuration.Redis;
using CellTracker.Api.Infrastructure.Distributor;
using CellTracker.Api.Ingestion.Queue;
using CellTracker.Api.Services.TelemetryRepository;
using CellTracker.Api.WorkerService.Validator;

namespace CellTracker.Api.WorkerService.Processor
{
    public class TelemetryProcessorService : BackgroundService
    {
        private readonly IRedisQueueService _redisQueueService;
        private readonly ITelemetryValidatorService _telemetryValidatorService;
        private readonly ITelemetryWriteService _telemetryWriteService;
        private readonly PeriodicTimer _timer;

        static int _incomingMessages = 0;
        static int _processedCount = 0;

        private readonly string _rawQueueKey;
        private readonly string _validatedQueueKey;
        private readonly string _distributionQueueKey;

        public TelemetryProcessorService(
            IRedisQueueService redisQueueService,
            ITelemetryValidatorService telemetryValidatorService,
            ITelemetryWriteService telemetryWriteService)
        {
            _redisQueueService = redisQueueService;
            _telemetryValidatorService = telemetryValidatorService;
            _telemetryWriteService = telemetryWriteService;

            _rawQueueKey = RedisExtension.GetRawQueueKey();
            _validatedQueueKey = RedisExtension.GetValidatedQueueKey();
            _distributionQueueKey = RedisExtension.GetDistributionQueueKey();

            _timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var processingTask = ProcessQueueAsync(stoppingToken);
            var metricsTask = PrintMetricsAsync(stoppingToken);

            await Task.WhenAll(processingTask, metricsTask);
        }


        protected async Task ProcessQueueAsync(CancellationToken stoppingToken)
        {
            //TODO: Delete later. Just for testing purposes, not to have so many entries.
            //await _telemetryWriteService.DeleteAllTelemetryData();

            SaveTelemetryDataPeriodicAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var telemetryData = await _redisQueueService.DequeueAsync(_rawQueueKey, stoppingToken);
                    
                    if (telemetryData == null)
                    {
                        // There's no item in the queue, so continue
                        continue;
                    }

                    if (!await _telemetryValidatorService.ValidateAsync(telemetryData))
                    {
                        // Todo: handling invalid messages
                        continue;
                    }

                    await _redisQueueService.EnqueueAsync(_validatedQueueKey,telemetryData, stoppingToken);
                    await _redisQueueService.EnqueueAsync(_distributionQueueKey,telemetryData, stoppingToken);

                    // The item is valid, send to client
                    //await _telemetryDistributorService.SendGroupAsync(telemetryData.WorkStationId, "Message", telemetryData);
                    //await _telemetryDistributorService.SendGroupAsync("loginGroup", "Message", telemetryData);
                    //await _telemetryDistributorService.SendObjectAsync("Message", telemetryData);

                    Interlocked.Increment(ref _incomingMessages);
                }
                catch (Exception ex)
                {
                    // Exception and handling
                    Console.WriteLine("Error processing telemetry");
                }
            }
        }
        private async Task PrintMetricsAsync(CancellationToken stoppingToken)
        {
            // Displaying metrics on UI
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Currently processed messages: {_incomingMessages - _processedCount} - All incoming messages: {_incomingMessages}");
                _processedCount = _incomingMessages;

                await Task.Delay(3000, stoppingToken);
            }
        }

        private async void SaveTelemetryDataPeriodicAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync())
            {                
                _telemetryWriteService.SaveTelemetryBatchAsync(stoppingToken);
            }
        }
    }
}
