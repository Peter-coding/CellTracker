using CellTracker.Api.Configuration.Extension;
using CellTracker.Api.Endpoint;
using CellTracker.Api.Endpoints;
using CellTracker.Api.ExceptionHandler;
using CellTracker.Api.Services.SignalR;

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

            

            app.MapHub<SignalRHub>("/hub");

            app.MapAuthEndpoint("Authentication");
            //Turning on for our own GlobalExceptionHandler
            app.UseExceptionHandler();

            app.MapOperatorEndpoint("operator");
            app.MapTelemetryEndpoint("telemetry");
            app.MapFactoryConfiguratorEndpoint("factoryConfigurator");

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
