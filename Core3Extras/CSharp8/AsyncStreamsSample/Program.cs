using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreamsSample
{
    class Program
    {
        static async Task Main()
        {
            await SimpleAsyncStreamAsync();
            // await AsyncStreamWithCancellation();
        }

        private static async Task AsyncStreamWithCancellation()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("cancellation requested"));
            cts.CancelAfter(5000);

            try
            {

                var aDevice = new ADevice();
                await foreach (var x in aDevice.GetSensorData1().WithCancellation(cts.Token))
                {
                    Console.WriteLine($"{x.Value1} {x.Value2}");
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task SimpleAsyncStreamAsync()
        {
            var aDevice = new ADevice();
            await foreach (var x in aDevice.GetSensorData())
            {
                Console.WriteLine($"{x.Value1} {x.Value2}");
            }
        }
    }
}
