using CellTracker.MqttIngestion.Configuration.Extension;


namespace CellTracker.MqttIngestion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.RegisterWorkerServicesExtension(builder.Configuration);

            var host = builder.Build();

            host.Run();
        }
    }
}