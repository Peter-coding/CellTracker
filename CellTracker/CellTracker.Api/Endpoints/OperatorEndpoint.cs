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
            app.MapPost($"{path}/Add", AddOperatorTask);
            app.MapGet($"{path}/GetAll", GetAllTasks);
            app.MapGet($"{path}/Get", GetTaskById);
            app.MapDelete($"{path}/Delete", RemoveTaskById);
            app.MapPut($"{path}/Update", UpdateTask);


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
