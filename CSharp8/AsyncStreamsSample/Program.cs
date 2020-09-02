using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreamsSample
{
    class Program
    {
        static async Task Main()
        {
            // await UseEnumeratorAsync();
            // await WhileLoopAsync();
            await WithCancellationAsync();
        }

        private static async Task UseEnumeratorAsync()
        {
            var aDevice = new ADevice();

            await foreach (var x in aDevice.GetSensorData1())
            {
                Console.WriteLine($"{x.Value1} {x.Value2}");
            }
        }

        private static async Task WhileLoopAsync()
        {
            var aDevice = new ADevice();

            IAsyncEnumerable<SensorData> dataStream = aDevice.GetSensorData1();
            await using IAsyncEnumerator<SensorData> enumerator = dataStream.GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                var sensorData = enumerator.Current;
                Console.WriteLine($"{sensorData.Value1} {sensorData.Value2}");
            }
        }

        private static async Task WithCancellationAsync()
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(5000);
                var aDevice = new ADevice();

                await foreach (var x in aDevice.GetSensorData2().WithCancellation(cts.Token))
                {
                    Console.WriteLine($"{x.Value1} {x.Value2}");
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
