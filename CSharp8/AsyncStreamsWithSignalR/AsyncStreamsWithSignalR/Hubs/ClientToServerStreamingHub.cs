using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using StreamSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AsyncStreamsWithSignalR.Hubs
{
    public class ClientToServerStreamingHub : Hub
    {
        private readonly ILogger _logger;

        public ClientToServerStreamingHub(ILogger<ClientToServerStreamingHub> logger)
        {
            _logger = logger;
        }

        // v3 - upload async streams
        public async Task StartStream(string streamName, ChannelReader<SomeData> reader)
        {
            while (await reader.WaitToReadAsync(Context.ConnectionAborted))
            {
                while (reader.TryRead(out SomeData data))
                {
                    _logger.LogTrace($"received in StreamingHub {data}");
                }
            }
        }

        // v4- upload async streams
        public async Task StartStream2(string streamName, IAsyncEnumerable<SomeData> stream)
        {
            Console.WriteLine($"Receive stream {streamName}");
            await foreach (var item in stream)
            {
                _logger.LogTrace($"received {item}");
            }
        }
    }
}
