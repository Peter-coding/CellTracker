using CellTracker.Api.Configuration.Extension;
using CellTracker.Api.Endpoint;
using CellTracker.Api.Endpoints;
using System.Threading.Tasks;

namespace CellTracker.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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

                await app.SeedInitialDataAsync();
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapOperatorEndpoint("operator");
            app.MapAuthEndpoint("Auth");

            app.Run();
        }
    }
}
