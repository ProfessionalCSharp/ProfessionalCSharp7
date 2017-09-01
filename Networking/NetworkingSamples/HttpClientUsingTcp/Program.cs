using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientUsingTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            Task<string> t1 = RequestHtmlAsync(args[0]);
            Console.WriteLine(t1.Result);
            Console.ReadLine();
        }

        private static void ShowUsage() =>
            Console.WriteLine("Usage: HttpClientUsingTcp hostname");

        private const int ReadBufferSize = 1024;
        public static async Task<string> RequestHtmlAsync(string hostname)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(hostname, 80);
                   
                    NetworkStream stream = client.GetStream();
                    string header = "GET / HTTP/1.1\r\n" +
                        $"Host: {hostname}:80\r\n" +
                        "Connection: close\r\n" +
                        "\r\n";
                    byte[] buffer = Encoding.UTF8.GetBytes(header);
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                    await stream.FlushAsync();
                    var ms = new MemoryStream();
                    buffer = new byte[ReadBufferSize];
                    int read = 0;
                    do
                    {
                        read = await stream.ReadAsync(buffer, 0, ReadBufferSize);
                        ms.Write(buffer, 0, read);
                        Array.Clear(buffer, 0, buffer.Length);
                    } while (read > 0);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
