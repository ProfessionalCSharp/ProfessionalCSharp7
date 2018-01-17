using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    public interface IGroupClient
    {
        void MessageToGroup(string groupName, string name, string message);
    }

    public class GroupChatHub : Hub<IGroupClient>
    {
        public Task AddGroup(string groupName) =>
            Groups.AddAsync(Context.ConnectionId, groupName);

        public Task LeaveGroup(string groupName) =>
            Groups.RemoveAsync(Context.ConnectionId, groupName);

        public void Send(string group, string name, string message) =>
            Clients.Group(group).MessageToGroup(group, name, message);

        public override Task OnConnectedAsync() => base.OnConnectedAsync();

        public override Task OnDisconnectedAsync(Exception exception) => base.OnDisconnectedAsync(exception);
    }
}
