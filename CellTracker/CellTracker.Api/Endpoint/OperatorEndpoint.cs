using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Services.Operator;
using CellTracker.Api.Services.TelemetryRepository;

namespace CellTracker.Api.Endpoint
{
    public static class OperatorEndpoint
    {
        public static void MapOperatorEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";

            //app.MapPost($"{path}/text", function);
            app.MapPost($"{path}/AddOperatorTask", AddOperatorTask);
            app.MapGet($"{path}/GetAllOperatorTasks", GetAllTasks);
            app.MapGet($"{path}/GetTaskById", GetTaskById);
            app.MapDelete($"{path}/RemoveTaskById", RemoveTaskById);
            app.MapPut($"{path}/UpdateTask", UpdateTask);


        }
        public static OperatorTask AddOperatorTask(IOperatorService service, OperatorTask task)
        {
            return service.AddOperatorTask(task);
        }

        public static IQueryable<OperatorTask> GetAllTasks(IOperatorService service)
        {
            return service.GetAllOperatorTasks();
        }

        public static OperatorTask GetTaskById(IOperatorService service, Guid id)
        {
            return service.GetOperatorTaskById(id).Result;
        }

        public static void RemoveTaskById(IOperatorService service, Guid id)
        {
            service.RemoveOperatorTaskById(id);
        }

        public static void UpdateTask(IOperatorService service, OperatorTask task)
        {
            service.UpdateOperatorTask(task);
        }
    }
}
