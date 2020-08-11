using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using StreamSample.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AsyncStreamsWithSignalR.Hubs
{
    public class ServerToClientStreamingHub : Hub
    {
        // v2 - async streams
        public async IAsyncEnumerable<SomeData> GetSomeDataWithAsyncStreams(int count, int delay, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            for (int i = 0; i < count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(delay);
                yield return new SomeData { Value = i };
            }
        }

        // v1 - using ChannelReader
        public ChannelReader<SomeData> GetSomeDataWithChannelReader(
            int count, 
            int delay, 
            CancellationToken cancellationToken)
        {
            var channel = Channel.CreateUnbounded<SomeData>();
             _ = WriteItemsAsync(channel.Writer, count, delay, cancellationToken);

            return channel.Reader;
        }

        private async Task WriteItemsAsync(
            ChannelWriter<SomeData> writer,
            int count,
            int delay,
            CancellationToken cancellationToken)
        {
            try
            {
                for (var i = 0; i < count; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await writer.WriteAsync(new SomeData { Value = i });

                    await Task.Delay(delay, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                writer.TryComplete(ex);
            }

            writer.TryComplete();
        }
    }
}
