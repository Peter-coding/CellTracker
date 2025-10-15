using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.Operator;

namespace CellTracker.Api.Configuration.Extension
{
    public static class ServiceRegistrations
    {
        public static WebApplicationBuilder RegisterServicesExtension(
            this WebApplicationBuilder builder,
            IConfiguration configuration)
        {
            // DbContext Registration
            builder.Services.RegisterDbContextExtension();

            builder.AddAuthenticationServices();

            //Add more Repositories later to inject it into UnitOfWork
            builder.Services.AddScoped<IRepository<OperatorTask>, OperatorTaskRepository>();

            // Add service
            builder.Services.AddScoped<IOperatorService, OperatorService>();

            // Add UnitOfWork pattern
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            return builder;
        }
    }
}
