namespace CellTracker.Api.Infrastructure.Logging
{
    public interface ILoggingService
    {
        void LogInformation(string functionName, string typeName, string logMessage);
        void LogWarning(string functionName, string typeName, string logMessage);
        void LogError(string functionName, string typeName, string logMessage);
        void LogException(Exception exception, string functionName, string typeName, string logMessage);
    }
}
