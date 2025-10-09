using CellTracker.Api.Configuration.Extension;
using CellTracker.Api.Endpoint;

namespace CellTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.RegisterServicesExtension(builder.Configuration);
            
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

            app.MapOperatorEndpoint("operator");
            app.MapTelemetryEndpoint("telemetry");

            app.Run();
        }
    }
}
