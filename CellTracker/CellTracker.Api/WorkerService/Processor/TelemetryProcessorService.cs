using CellTracker.Api.Ingestion.Queue;
using CellTracker.Api.WorkerService.Validator;

namespace CellTracker.Api.WorkerService.Processor
{
    public class TelemetryProcessorService : BackgroundService
    {
        private readonly IRedisQueueService _redisQueueService;
        private readonly ITelemetryValidatorService _telemetryValidatorService;

        static int _incomingMessages = 0;
        static int _processedCount = 0;
        public TelemetryProcessorService(
            IRedisQueueService redisQueueService,
            ITelemetryValidatorService telemetryValidatorService)
        {
            _redisQueueService = redisQueueService;
            _telemetryValidatorService = telemetryValidatorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var processingTask = ProcessQueueAsync(stoppingToken);
            var metricsTask = PrintMetricsAsync(stoppingToken);

            await Task.WhenAll(processingTask, metricsTask);
        }


        protected async Task ProcessQueueAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var telemetryData = await _redisQueueService.DequeueAsync(stoppingToken);

                    if (telemetryData == null)
                    {
                        // There's no item in the queue, so continue
                        continue;
                    }

                    if (!await _telemetryValidatorService.ValidateAsync(telemetryData))
                    {
                        // Todo validation handling
                        continue;
                    }

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

                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
