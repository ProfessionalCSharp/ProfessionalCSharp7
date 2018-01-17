using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggingSample
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
            try
            {
                _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync started with url {0}", url);
                var client = new HttpClient();

                string result = await client.GetStringAsync(url);
                _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync completed, received {0} characters", result.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Networking, ex, "Error in NetworkRequestSampleAsync, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
            }
        }
    }
}
