using MQTTnet;

namespace CellTracker.Api.Configuration.MqttClient
{
    public static class MqttClientExtensions
    {
        public static IServiceCollection AddMqttClientOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var mqttHost = Environment.GetEnvironmentVariable("MQTT_BROKER_HOST") ?? "mosquitto";

            var mqttPort = int.TryParse(Environment.GetEnvironmentVariable("MQTT_BROKER_PORT"), out var port) ? port : 1883;

            // Transient, because TestMqttService uses this too
            services.AddTransient<IMqttClient>(sp =>
            {
                var factory = new MqttClientFactory();

                var client = factory.CreateMqttClient();

                return client;
            });

            // Transient, because TestMqttService uses this too
            services.AddTransient<MqttClientOptions>(sp =>
            {

                return new MqttClientOptionsBuilder()
                    .WithTcpServer(mqttHost, mqttPort)
                    .Build();
            });

            return services;
        }
    }
}
