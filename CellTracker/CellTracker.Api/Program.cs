using CellTracker.Api.Configuration.Extension;
using CellTracker.Api.Endpoint;
using CellTracker.Api.Endpoints;
using CellTracker.Api.ExceptionHandler;

namespace CellTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.RegisterServicesExtension(builder.Configuration);
            
            builder.Services.AddSignalR();

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
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();   

            //Turning on for our own GlobalExceptionHandler
            app.UseExceptionHandler();

            app.MapOperatorEndpoint("operator");
            app.MapTelemetryEndpoint("telemetry");

            app.Run();
        }
    }
}
