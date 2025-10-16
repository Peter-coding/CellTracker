using CellTracker.Api.Services.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CellTracker.Api.Ingestion.Distributor
{
    public class TelemetryDistributorService : ITelemetryDistributorService
    {
        private readonly IHubContext<SignalRHub> _hub;

        public TelemetryDistributorService(IHubContext<SignalRHub> hub)
        {
            _hub = hub;
        }

        public async Task SendAsync(string method)
        {
            await _hub.Clients.All.SendAsync(method);
        }

        public async Task SendObjectAsync(string method, object obj)
        {
            await _hub.Clients.All.SendAsync(method, obj);
        }

        public async Task SendGroupAsync(string group, string method, object? obj)
        {
            await _hub.Clients.Group(group).SendAsync(method, obj);
        }
    }
}
