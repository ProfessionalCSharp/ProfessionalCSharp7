using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventSourceSampleAnnotations
{
    class Program
    {
        public static void GenerateManifest()
        {
            string schema = SampleEventSource.GenerateManifest(
              typeof(SampleEventSource), ".");
            File.WriteAllText("sampleeventsource.xml", schema);
        }

        static async Task Main(string[] args)
        {
            SampleEventSource.Log.Startup();
            GenerateManifest();
            Console.WriteLine($"Log Guid: {SampleEventSource.Log.Guid}");
            Console.WriteLine($"Name: {SampleEventSource.Log.Name}");
            await NetworkRequestSampleAsync();
            Console.ReadLine();
        }

        private static async Task NetworkRequestSampleAsync()
        {
            try
            {
                var client = new HttpClient();
                string url = "http://www.cninnovation.com";
                SampleEventSource.Log.CallService(url);
                string result = await client.GetStringAsync(url);
                SampleEventSource.Log.CalledService(url, result.Length);
                Console.WriteLine("Complete.................");
            }
            catch (Exception ex)
            {
                SampleEventSource.Log.ServiceError(ex.Message, ex.HResult);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
