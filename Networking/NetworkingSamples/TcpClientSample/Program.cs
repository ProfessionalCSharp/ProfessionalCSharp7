using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientSample
{
    class Program
    {
        private const string Host = "localhost";
        private const int Port = 8000;

        static async Task Main()
        {
            await SendAndReceiveAsync();
            Console.ReadLine();
        }

        public static async Task SendAndReceiveAsync()
        {
            using (var client = new TcpClient())
            {
                await client.ConnectAsync(Host, Port);
                using (NetworkStream stream = client.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.ASCII, 1024, leaveOpen: true))
                using (var reader = new StreamReader(stream, Encoding.ASCII, true, 1024, leaveOpen: true))
                {
                    writer.AutoFlush = true;
                    string line = string.Empty;
                    do
                    {
                        Console.WriteLine("enter a string, bye to exit");
                        line = Console.ReadLine();
                        await writer.WriteLineAsync(line);
                       
                        string result = await reader.ReadLineAsync();
                        Console.WriteLine($"received {result} from server");
                    } while (line != "bye");

                    Console.WriteLine("so long, and thanks for all the fish");
                }
            }            
        }
    }
}
