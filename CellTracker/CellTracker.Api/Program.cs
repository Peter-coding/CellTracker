using CellTracker.Api.Configuration.Extension;
using CellTracker.Api.Endpoint;
using CellTracker.Api.Endpoints;
using CellTracker.Api.ExceptionHandler;
using CellTracker.Api.Services.SignalR;
using System.Security.Cryptography.X509Certificates;

namespace CellTracker.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.RegisterServicesExtension(builder.Configuration);
            
            builder.Services.AddCors();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                //app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Tracker Api");
                });
                app.ApplyMigrations();
                await app.SeedInitialDataAsync();
            }
            app.UseHttpsRedirection();

            var certPath = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path");
            var certPassword = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password");

            var cert = new X509Certificate2(certPath, certPassword);

            Console.WriteLine("=== HTTPS CERTIFICATE ===");
            Console.WriteLine($"Subject     : {cert.Subject}");
            Console.WriteLine($"Issuer      : {cert.Issuer}");
            Console.WriteLine($"Thumbprint  : {cert.Thumbprint}");
            Console.WriteLine($"Valid From  : {cert.NotBefore}");
            Console.WriteLine($"Valid Until : {cert.NotAfter}");
            Console.WriteLine($"Cert file name: {Path.GetFileName(certPath)}");
            Console.WriteLine("=========================");


            app.MapHub<SignalRHub>("/hub");

            app.MapAuthEndpoint("Auth");
            //Turning on for our own GlobalExceptionHandler
            app.UseExceptionHandler();

            app.MapOperatorEndpoint("Operator");
            app.MapTelemetryEndpoint("Telemetry");
            app.MapFactoryConfiguratorEndpoint("Configurator");
            app.MapSimulationEndpoint("Simulation");
            app.MapStatisticsEndpoint("Statistics");

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4300"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
