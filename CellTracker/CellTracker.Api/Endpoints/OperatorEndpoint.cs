using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Services.OperatorTaskService;
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
        public static OperatorTask AddOperatorTask(IOperatorTaskService service, OperatorTask task)
        {
            return service.AddOperatorTask(task);
        }

        public static IQueryable<OperatorTask> GetAllTasks(IOperatorTaskService service)
        {
            return service.GetAllOperatorTasks();
        }

        public static OperatorTask GetTaskById(IOperatorTaskService service, Guid id)
        {
            return service.GetOperatorTaskById(id).Result;
        }

        public static void RemoveTaskById(IOperatorTaskService service, Guid id)
        {
            service.RemoveOperatorTaskById(id);
        }

        public static void UpdateTask(IOperatorTaskService service, OperatorTask task)
        {
            service.UpdateOperatorTask(task);
        }
    }
}
