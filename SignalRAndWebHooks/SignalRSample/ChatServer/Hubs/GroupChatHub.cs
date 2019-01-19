using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    public interface IGroupClient
    {
        Task MessageToGroup(string groupName, string name, string message);
    }

    public class GroupChatHub : Hub<IGroupClient>
    {
        public Task AddGroup(string groupName)
            => Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        public Task LeaveGroup(string groupName)
            => Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        public Task Send(string group, string name, string message) =>
            Clients.Group(group).MessageToGroup(group, name, message);

        public override Task OnConnectedAsync() => base.OnConnectedAsync();

        public override Task OnDisconnectedAsync(Exception exception) => base.OnDisconnectedAsync(exception);
    }
}
