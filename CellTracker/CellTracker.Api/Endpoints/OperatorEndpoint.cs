using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.OperatorTask;
using CellTracker.Api.Services.OperatorTaskService;
using CellTracker.Api.Services.TelemetryRepository;
using System.Threading.Tasks;

namespace CellTracker.Api.Endpoint
{
    public static class OperatorEndpoint
    {
        public static void MapOperatorEndpoint(this IEndpointRouteBuilder app, string pathPrefix)
        {
            var path = $"/{pathPrefix}";

            app.MapPost($"{path}/Add", AddOperatorTask);
            app.MapGet($"{path}/GetAll", GetAllTasks);
            app.MapGet($"{path}/Get", GetTaskById);
            app.MapDelete($"{path}/Delete", RemoveTaskById);
            app.MapPut($"{path}/Update", UpdateTask);
        }
        public static async Task<IResult> AddOperatorTask(IOperatorTaskService service, CreateOperatorTaskDto taskDto)
        {
            var created = await service.AddOperatorTask(taskDto);
            if(created != null)
            {
                return Results.Ok(created);
            }
            return Results.BadRequest("OperatorTask could not be created");
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
