using Microsoft.AspNetCore.SignalR.Client;
using StreamSample.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        private static HubConnection? s_hubConnection;
        static async Task Main()
        {
            Console.WriteLine("client - wait for service...");
            Console.ReadLine();

            await ServerToClientStreamingAsync();
            // await ClientToServerStreamingAsync();
        }

        private static async Task ClientToServerStreamingAsync()
        {
            static async IAsyncEnumerable<SomeData> clientStreamData()
            {
                for (var i = 0; i < 20; i++)
                {
                    await Task.Delay(2000);
                    var data = new SomeData() { Value = i };
                    yield return data;
                }
            }

            s_hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hubs/uploadstream")
                .Build();
            var cts = new CancellationTokenSource();
            await s_hubConnection.StartAsync(cts.Token);

            await s_hubConnection.SendAsync("StartStream2", "Sample Stream", clientStreamData());

            Console.WriteLine("SendAsync completed");
            Console.ReadLine();

        }

        private static async Task ServerToClientStreamingAsync()
        {
            s_hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hubs/stream")
                .Build();

            s_hubConnection.Closed += async (ex) =>
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("restart");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await s_hubConnection.StartAsync();
            };

            var cts = new CancellationTokenSource();
            await s_hubConnection.StartAsync(cts.Token);

            // read from the hub using ChannelReader
            //var channel = await s_hubConnection.StreamAsChannelAsync<SomeData>("GetSomeDataWithChannelReader", 100, 1000, cts.Token);
            //while (await channel.WaitToReadAsync())
            //{
            //    while (channel.TryRead(out SomeData data))
            //    {
            //        Console.WriteLine($"received {data}");
            //    }
            //}

            //// read from the hub using async streams
            var stream = s_hubConnection.StreamAsync<SomeData>("GetSomeDataWithChannelReader", 20, 100, cts.Token);
            await foreach (var d in stream)
            {
                Console.WriteLine($"received {d}");
            }

            // https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/signalr/streaming.md
        }
    }
}
