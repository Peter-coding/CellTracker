using CellTracker.Api.Infrastructure.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace CellTracker.Api.ExceptionHandler
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILoggingService _logger;
        private readonly IProblemDetailsService _problemDetailsService;

        public GlobalExceptionHandler(ILoggingService logger, IProblemDetailsService problemDetailsService)
        {
            _logger = logger;
            _problemDetailsService = problemDetailsService;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            //TODO: I think method name cannot be logged here. Discuss what should be logged.
            _logger.LogError("",
                exception.GetType().Name,
                exception.Message);

            httpContext.Response.StatusCode = exception switch
            {
                 ApplicationException => StatusCodes.Status400BadRequest,
                 UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                 KeyNotFoundException => StatusCodes.Status404NotFound,
                 _ => StatusCodes.Status500InternalServerError
            };

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "Error occured",
                    Detail = exception.Message
                }
            });

        }
    }
}
