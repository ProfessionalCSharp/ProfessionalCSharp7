using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggingSample
{
    class SampleController
    {
        private ILogger<SampleController> _logger;
        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        public async Task NetworkRequestSampleAsync(string url)
        {
            try
            {
                _logger.LogInformation("NetworkRequestSample started", url);
                var client = new HttpClient();

                string result = await client.GetStringAsync(url);
                _logger.LogInformation("NetworkRequestSample completed", result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"NetworkRequestSample {ex.Message}", ex.Message, ex.HResult);
                Console.WriteLine(ex.Message);
            }
        }

        public async Task NetworkRequestSample2Async(string url)
        {
            using (_logger.BeginScope("NetworkRequestSample"))
            {
                try
                {
                    _logger.Log(LogLevel.Trace, new EventId(42), url, null, (state, ex) => $"message: {state}, {ex?.Message}");
                    _logger.LogInformation("NetworkRequestSample started {0}", url);
                    var client = new HttpClient();
                    string result = await client.GetStringAsync(url);
                    _logger.LogInformation("NetworkRequestSample completed {0} --", result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"NetworkRequestSample {ex.Message}", ex.Message, ex.HResult);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
