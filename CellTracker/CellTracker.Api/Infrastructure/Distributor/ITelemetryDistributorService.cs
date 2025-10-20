namespace CellTracker.Api.Infrastructure.Distributor
{
    public interface ITelemetryDistributorService
    {
        Task SendAsync(string method);
        Task SendObjectAsync(string method, object obj);
        Task SendGroupAsync(string group, string method, object? obj);
    }
}
