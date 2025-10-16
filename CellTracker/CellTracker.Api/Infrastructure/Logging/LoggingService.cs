using CellTracker.Api.Infrastructure.UserIdentiy;
using Serilog.Context;

namespace CellTracker.Api.Infrastructure.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;
        private readonly ICurrentUserService _currentUserService;
        public LoggingService(
            ILogger<LoggingService> logger
          , ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public void LogInformation(string functionName, string typeName, string logMessage)
        {
            Log("Information", functionName, typeName, logMessage);
        }
        public void LogWarning(string functionName, string typeName, string logMessage)
        {
            Log("Warning", functionName, typeName, logMessage);
        }
        public void LogError(string functionName, string typeName, string logMessage)
        {
            Log("Error", functionName, typeName, logMessage);
        }

        private void Log(string logType, string functionName, string typeName, string logMessage)
        {
            var username = _currentUserService.GetUsername();

            using (LogContext.PushProperty("Function", functionName))
            using (LogContext.PushProperty("UserName", username))
            using (LogContext.PushProperty("Type", typeName))
            {

                if (logType == "Information")
                {
                    _logger.LogInformation(logMessage);
                }
                else if (logType == "Warning")
                {
                    _logger.LogWarning(logMessage);
                }
                else if (logType == "Error")
                {
                    _logger.LogError(logMessage);
                }
            }
        }

        public void LogException(Exception exception, string functionName, string typeName, string logMessage)
        {
            var username = _currentUserService.GetUsername();
            using (LogContext.PushProperty("Function", functionName))
            using (LogContext.PushProperty("UserName", username))
            using (LogContext.PushProperty("Type", typeName))
            {
                _logger.LogError(exception, logMessage);
            }
        }
    }
}
