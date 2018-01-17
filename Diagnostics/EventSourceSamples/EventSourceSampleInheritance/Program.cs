using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventSourceSampleInheritance
{
    class Program
    {
        static async Task Main()
        {
            SampleEventSource.Log.Startup();
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
