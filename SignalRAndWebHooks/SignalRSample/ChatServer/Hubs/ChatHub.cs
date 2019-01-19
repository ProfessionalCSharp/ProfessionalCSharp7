using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    public class ChatHub : Hub
    {
        public Task Send(string name, string message) =>
            Clients.All.SendAsync("BroadcastMessage", name, message);
    }
}
