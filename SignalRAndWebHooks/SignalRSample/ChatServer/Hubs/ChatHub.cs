using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.SendAsync("BroadcastMessage", name, message);
        }
    }
}
