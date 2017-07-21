using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AsyncHistory
{

    class Program
    {
        private const string url = "http://www.cninnovation.com";
        static async Task Main()
        {
            SynchronizedAPI();
            AsynchronousPattern();
            EventBasedAsyncPattern();
            await TaskBasedAsyncPatternAsync();
            Console.ReadLine();
        }

        private static async Task TaskBasedAsyncPatternAsync()
        {
            Console.WriteLine(nameof(TaskBasedAsyncPatternAsync));
            using (var client = new WebClient())
            {
                string content = await client.DownloadStringTaskAsync(url);
                Console.WriteLine(content.Substring(0, 100));
                Console.WriteLine();
            }
        }

        private static void EventBasedAsyncPattern()
        {
            Console.WriteLine(nameof(EventBasedAsyncPattern));
            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    Console.WriteLine(e.Result.Substring(0, 100));
                };
                client.DownloadStringAsync(new Uri(url));
                Console.WriteLine();
            }
        }

        private static void AsynchronousPattern()
        {
            Console.WriteLine(nameof(AsynchronousPattern));
            WebRequest request = WebRequest.Create(url);
            IAsyncResult result = request.BeginGetResponse(ReadResponse, null);

            void ReadResponse(IAsyncResult ar)
            {
                using (WebResponse response = request.EndGetResponse(ar))
                {
                    Stream stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                    Console.WriteLine();
                }
            }
        }

        private static void SynchronizedAPI()
        {
            Console.WriteLine(nameof(SynchronizedAPI));
            using (var client = new WebClient())
            {
                string content = client.DownloadString(url);
                Console.WriteLine(content.Substring(0, 100));
            }
            Console.WriteLine();
        }
    }
}
