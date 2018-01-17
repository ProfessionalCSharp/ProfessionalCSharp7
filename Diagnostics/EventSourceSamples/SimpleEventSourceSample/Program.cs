using System;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleEventSourceSample
{
    class Program
    {
        private static EventSource sampleEventSource = new EventSource("Wrox-SimpleEventSourceSample");

        static async Task Main()
        {
            Console.WriteLine($"Log Guid: {sampleEventSource.Guid}");
            Console.WriteLine($"Name: {sampleEventSource.Name}");

            sampleEventSource.Write("Startup", new { Info = "started app" });
            await NetworkRequestSampleAsync();
            Console.ReadLine();
            sampleEventSource.Dispose();
        }

        private static async Task NetworkRequestSampleAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://www.cninnovation.com";
                    sampleEventSource.Write("Network", new { Info = $"calling {url}" });

                    string result = await client.GetStringAsync(url);
                    sampleEventSource.Write("Network", new { Info = $"completed call to {url}, result string length: {result.Length}" });
                }
                Console.WriteLine("Complete.................");
            }
            catch (Exception ex)
            {
                sampleEventSource.Write("Network Error", new EventSourceOptions { Level = EventLevel.Error }, new { Message = ex.Message, Result = ex.HResult });
                Console.WriteLine(ex.Message);
            }
        }
    }
}
