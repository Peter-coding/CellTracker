using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CellTracker.Api.Services.SignalR
{
    //TODO: Discuss what name this class and Folder should have
    public sealed class SignalRHub : Hub
    {
        private static int _conns = 0;

        //static List<string> ids = new List<string>();
        private static readonly ConcurrentDictionary<string, HashSet<string>> GroupMembers = new ConcurrentDictionary<string, HashSet<string>>();
        public override Task OnConnectedAsync()
        {
            _conns++;
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            Console.WriteLine(_conns);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _conns--;
            Clients.Caller.SendAsync("Disconnected", Context.ConnectionId);
            Console.WriteLine(_conns);


            foreach (var group in GroupMembers)
            {
                if (group.Value.Contains(Context.ConnectionId))
                {
                    // Remove the client from the group
                    RemoveFromGroup(group.Key, Context.ConnectionId);

                    // Optionally log the removal
                    Console.WriteLine($"Removed from group: {group.Key}, ConnectionId: {Context.ConnectionId}");
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine("Added to group: " + groupName + " " + Context.ConnectionId);
            // Add the connection to the group list
            GroupMembers.AddOrUpdate(groupName,
                new HashSet<string> { Context.ConnectionId },
                (key, existingGroup) => { existingGroup.Add(Context.ConnectionId); return existingGroup; });

            GetGroupMembers();

            // Optionally notify other group members that someone has joined
            //await Clients.Group(groupName).SendAsync("UserJoined", Context.ConnectionId);
        }

        // Method to remove a client from a group and update the tracking
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine("Removed from group: " + groupName + " " + Context.ConnectionId);
            RemoveFromGroup(groupName, Context.ConnectionId);

            GetGroupMembers();
            // Optionally notify other group members that someone has left
            //await Clients.Group(groupName).SendAsync("UserLeft", Context.ConnectionId);
        }

        public Task AcknowledgeHappened()
        {
            return Clients.All.SendAsync("AcknowledgeHappened");
        }

        public Task SendToGroupAsync(string group, string method, object obj)
        {
            return Clients.Group(group).SendAsync(method, obj);
        }

        // Helper method to remove a connection from a group
        private void RemoveFromGroup(string groupName, string connectionId)
        {
            if (GroupMembers.TryGetValue(groupName, out HashSet<string>? value))
            {
                var group = value;
                group.Remove(connectionId);

                // Clean up empty groups if needed
                if (group.Count == 0)
                {
                    GroupMembers.TryRemove(groupName, out _);
                }
            }
        }

        // Method to get the list of connections in a group
        public static HashSet<string> GetGroupMembers()
        {
            if (GroupMembers.TryGetValue("InputGroup", out HashSet<string>? members1))
            {
                Console.WriteLine("InputGroup members:-----------");
                foreach (var connectionId in members1)
                {
                    Console.WriteLine(connectionId);
                }

                Console.WriteLine("-------------");
                Console.WriteLine();
                //return members1;
            }

            if (GroupMembers.TryGetValue("ReactorGroup", out HashSet<string>? members2))
            {
                Console.WriteLine("Reactor members:-----------");
                foreach (var connectionId in members2)
                {
                    Console.WriteLine(connectionId);
                }

                Console.WriteLine("-------------");
                Console.WriteLine();
                //return members2;
            }

            if (GroupMembers.TryGetValue("OutputGroup", out HashSet<string>? members3))
            {
                Console.WriteLine("Output members:-----------");
                foreach (var connectionId in members3)
                {
                    Console.WriteLine(connectionId);
                }

                Console.WriteLine("-------------");
                Console.WriteLine();
                //return members2;
            }

            if (GroupMembers.TryGetValue("MainGroup", out HashSet<string>? members4))
            {
                Console.WriteLine("Main members:-----------");
                foreach (var connectionId in members4)
                {
                    Console.WriteLine(connectionId);
                }

                Console.WriteLine("-------------");
                Console.WriteLine();
                //return members2;
            }

            return new HashSet<string>();
        }

        public static bool IsGroupNotEmpty(string groupName)
        {
            // Check if the group exists and has any members
            if (GroupMembers.TryGetValue(groupName, out var members))
            {
                return members.Count > 0; // Return true if there are members in the group
            }

            return false; // Return false if the group doesn't exist or has no members
        }
    }
}
