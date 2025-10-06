using Microsoft.AspNetCore.SignalR;

namespace CellTracker.Api.XyHub
{
    //TODO: Discuss what name this class and Folder should have
    public class SignalRHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
