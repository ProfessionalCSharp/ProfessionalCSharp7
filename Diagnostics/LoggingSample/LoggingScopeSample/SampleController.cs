using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggingScopeSample
{
    class SampleController
    {
        private readonly ILogger<SampleController> _logger;
        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
            _logger.LogTrace("ILogger injected into {0}", nameof(SampleController));
        }

        public async Task NetworkRequestSampleAsync(string url)
        {
            using (_logger.BeginScope("NetworkRequestSampleAsync, url: {0}", url))
            {
                try
                {
                    _logger.LogInformation(LoggingEvents.Networking, "Started");
                    var client = new HttpClient();

                    string result = await client.GetStringAsync(url);
                    _logger.LogInformation(LoggingEvents.Networking, "Completed with characters {0} received", result.Length);
                }
                catch (Exception ex)
                {
                    _logger.LogError(LoggingEvents.Networking, ex, "Error, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
                }
            }
        }
    }
}
