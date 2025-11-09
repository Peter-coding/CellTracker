using CellTracker.Api.Configuration.Redis;
using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Ingestion.Queue;
using MQTTnet;
using MQTTnet.Server;
using System.Text;

namespace CellTracker.Api.WorkerService.Ingestion
{
    public class TelemetryIngestionService : BackgroundService
    {
        private readonly ILogger<TelemetryIngestionService> _logger;
        private readonly IRedisQueueService _queue;
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _options;

        private readonly string _rawQueueKey;

        public TelemetryIngestionService(
            ILogger<TelemetryIngestionService> logger,
            IRedisQueueService queue,
            IMqttClient mqttClient,
            MqttClientOptions options,
            IConfiguration cfg)
        {
            _logger = logger;
            _queue = queue;
            _mqttClient = mqttClient;
            _options = options;

            _rawQueueKey = RedisExtension.GetRawQueueKey();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Event for recieving messages
            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string topic = e.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                //Console.WriteLine($"#Üzenet érkezett - Topic: {topic}, Tartalom: {payload}");

                var telemetry = new TelemetryData
                {
                    OperatorId = topic,
                    WorkStationId = payload,
                    TimeStamp = DateTime.UtcNow,
                };

                // Add message to queue
                await _queue.EnqueueAsync(_rawQueueKey, telemetry, stoppingToken);
            };

            await _mqttClient.ConnectAsync(_options, CancellationToken.None);

            Console.WriteLine("The MQTT client is connected.");

            await _mqttClient.SubscribeAsync("telemetry/test", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
            
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
