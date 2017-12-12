using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("wait for the server to start...");
            Console.ReadLine();
            var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri("ws://localhost:5000/chat/ws"), CancellationToken.None);
            Console.WriteLine("connected with server");

            var sender = Task.Run(async () => await SenderAsync(ws));
            var receiver = Task.Run(async () => await ReceiverAsync(ws));
            await Task.WhenAll(sender);
        }

        static async Task SenderAsync(ClientWebSocket socket)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                var bytes = Encoding.UTF8.GetBytes(line);
                await socket.SendAsync(bytes, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
            }
        }

        static async Task ReceiverAsync(ClientWebSocket socket)
        {
            var buffer = new byte[2048];
            bool stopped = false;
            while(!stopped)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
                    stopped = true;
                }
            }
        }
    }
}
