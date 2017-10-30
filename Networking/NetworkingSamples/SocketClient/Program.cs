using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                ShowUsage();
                return;
            }
            string hostName = args[0];
            int port;
            if (!int.TryParse(args[1], out port))
            {
                ShowUsage();
                return;
            }
            Console.WriteLine("press return when the server is started");
            Console.ReadLine();

            await SendAndReceiveAsync(hostName, port);
            Console.ReadLine();
        }

        private static void ShowUsage() =>
            Console.WriteLine("Usage: SocketClient server port");

        public static async Task SendAndReceiveAsync(string hostName, int port)
        {
            try
            {
                IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostName);
                IPAddress ipAddress = ipHost.AddressList.Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
                if (ipAddress == null)
                {
                    Console.WriteLine("no IPv4 address");
                    return;
                }

                using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    client.Connect(ipAddress, port);
                    Console.WriteLine("client successfully connected");
                    var stream = new NetworkStream(client);
                    var cts = new CancellationTokenSource();

                    Task tSender = SenderAsync(stream, cts);
                    Task tReceiver = Receiver(stream, cts.Token);
                    await Task.WhenAll(tSender, tReceiver);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task SenderAsync(NetworkStream stream, CancellationTokenSource cts)
        {
            Console.WriteLine("Sender task");
            while (true)
            {
                Console.WriteLine("enter a string to send, shutdown to exit");
                string line = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes($"{line}\r\n");
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
                if (string.Compare(line, "shutdown", ignoreCase: true) == 0)
                {
                    cts.Cancel();
                    Console.WriteLine("sender task closes");
                    break;
                }
            }
        }

        private const int ReadBufferSize = 1024;

        public static async Task Receiver(NetworkStream stream, CancellationToken token)
        {
            try
            {
                stream.ReadTimeout = 5000;
                Console.WriteLine("Receiver task");
                byte[] readBuffer = new byte[ReadBufferSize];
                while (true)
                {
                    Array.Clear(readBuffer, 0, ReadBufferSize);

                    int read = await stream.ReadAsync(readBuffer, 0, ReadBufferSize, token);
                    string receivedLine = Encoding.UTF8.GetString(readBuffer, 0, read);
                    Console.WriteLine($"received {receivedLine}");
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}