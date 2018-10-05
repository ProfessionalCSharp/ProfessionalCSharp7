using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketClient
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Client - wait for server");
            Console.ReadLine();
            await InitiateWebSocketCommunication("ws://localhost:2858/samplesockets");
            Console.WriteLine("Program end");
            Console.ReadLine();          
        }

        static async Task InitiateWebSocketCommunication(string address)
        {
            try
            {
                var webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(address), CancellationToken.None);

                await SendAndReceiveAsync(webSocket, "A");
                await SendAndReceiveAsync(webSocket, "B");
                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("SERVERCLOSE")), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
                var buffer = new byte[4096];
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                
                Console.WriteLine($"received for close: {result.CloseStatus} {result.CloseStatusDescription} {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye", CancellationToken.None);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task SendAndReceiveAsync(WebSocket webSocket, string term)
        {
            byte[] data = Encoding.UTF8.GetBytes($"REQUESTMESSAGES:{term}");
            var buffer = new byte[4096];

            await webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
            WebSocketReceiveResult result;
            bool sequenceEnd = false;
            do
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string[] dataReceived = Encoding.UTF8.GetString(buffer, 0, result.Count).Split(Environment.NewLine);
                foreach (var line in dataReceived)
                {
                    Console.WriteLine($"received {line}");
                    if (line.StartsWith("EOS"))
                    {
                        sequenceEnd = true;
                        Console.WriteLine("...ending sequence");
                    }
                }

            } while (!(result?.CloseStatus.HasValue ?? false) && !sequenceEnd);
        }
    }
}
