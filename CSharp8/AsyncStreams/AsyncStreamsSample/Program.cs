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
            await Demo4Async();
            // await Demo3Async();
            // await Demo2Async();
            // await Demo1Async();
        }

        private static async Task Demo4Async()
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(5000);
                var aDevice = new ADevice();

                await foreach (var x in aDevice.GetSensorData3().WithCancellation(cts.Token))
                {
                    Console.WriteLine($"{x.Value1} {x.Value2}");
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static async Task Demo3Async()
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

        private static async Task Demo2Async()
        {
            var aDevice = new ADevice();

            IAsyncEnumerable<SensorData> dataStream = aDevice.GetSensorData1();
            IAsyncEnumerator<SensorData> e = dataStream.GetAsyncEnumerator();
            try
            {
                while (await e.MoveNextAsync())
                {
                    var sensorData = e.Current;
                    Console.WriteLine($"{sensorData.Value1} {sensorData.Value2}");
                }
            }
            finally
            {
                await e.DisposeAsync();
            }
        }

        private static async Task Demo1Async()
        {
            var aDevice = new ADevice();

            await foreach (var x in aDevice.GetSensorData1())
            {
                Console.WriteLine($"{x.Value1} {x.Value2}");
            }
        }
    }
}
