using CellTracker.Api.Ingestion.Model;
using MQTTnet;
using System.Text.Json;

namespace CellTracker.Api.Services.TestMqtt
{
    public class TestMqttService : BackgroundService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _options;

        public TestMqttService(
            IMqttClient mqttClient,
            MqttClientOptions options)
        {
            _mqttClient = mqttClient;
            _options = options;
        }

        static int count = 0;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Loop for endless reconnecting till established connection
            while (!_mqttClient.IsConnected && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _mqttClient.ConnectAsync(_options, stoppingToken);
                    Console.WriteLine("MQTT client connected");
                }
                catch
                {
                    Console.WriteLine("Waiting for broker...");
                    await Task.Delay(1000, stoppingToken);
                }
            }

            // Loop for publishing random number of Mqtt messages to "telemetry/test" topic
            while (!stoppingToken.IsCancellationRequested)
            {
                var rnd = new Random();
                var num = rnd.Next(1, 3);

                for (int i = 0; i < num; i++)
                {
                    var telemetry = new TelemetryData
                    {
                        TimeStamp = DateTime.UtcNow,
                        WorkStationId = $"WS-{rnd.Next(1, 10)}",
                        OperatorId = $"OP-{rnd.Next(100, 999)}",
                        ProductId = $"PRD-{rnd.Next(1000, 9999)}",
                        IsCompleted = rnd.Next(0, 2) == 1,
                        Error = (byte)rnd.Next(0, 4)
                    };

                    string payload = JsonSerializer.Serialize(telemetry);
                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic("telemetry/test")
                        .WithPayload(payload)
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                        .Build();

                    await _mqttClient.PublishAsync(message, stoppingToken);
                }

                var nr = count += num;
                Console.WriteLine("Number of messages: " + nr);

                await Task.Delay(2000, stoppingToken);
                Console.Clear();
            }
        }
    }
}
