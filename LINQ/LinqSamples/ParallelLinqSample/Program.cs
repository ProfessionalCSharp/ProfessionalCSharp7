using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLinqSample
{
    class Program
    {
        static void Main()
        {
            IList<int> data = SampleData();
            LinqQuery(data);
            ExtensionMethods(data);
            UseAPartitioner(data);
            UseCancellation(data);
        }

        static void LinqQuery(IEnumerable<int> data)
        {
            Console.WriteLine(nameof(LinqQuery));
            var res = (from x in data.AsParallel()
                       where Math.Log(x) < 4
                       select x).Average();
            Console.WriteLine($"result from {nameof(LinqQuery)}: {res}");
            Console.WriteLine();
        }

        static void ExtensionMethods(IEnumerable<int> data)
        {
            Console.WriteLine(nameof(ExtensionMethods));
            var res = data.AsParallel()
                .Where(x => Math.Log(x) < 4)
                .Select(x => x).Average();

            Console.WriteLine($"result from {nameof(ExtensionMethods)}: {res}");
            Console.WriteLine();
        }

        static void UseAPartitioner(IList<int> data)
        {
            Console.WriteLine(nameof(UseAPartitioner));
            var res = (from x in Partitioner.Create(data, loadBalance: true).AsParallel()
                       where Math.Log(x) < 4
                       select x).Average();

            Console.WriteLine($"result from {nameof(UseAPartitioner)}: {res}");
            Console.WriteLine();
        }

        static void UseCancellation(IEnumerable<int> data)
        {
            Console.WriteLine(nameof(UseCancellation));
            var cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    var res = (from x in data.AsParallel().WithCancellation(cts.Token)
                               where Math.Log(x) < 4
                               select x).Average();

                    Console.WriteLine($"query finished, sum: {res}");
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            Console.WriteLine("query started");
            Console.Write("cancel? ");
            string input = Console.ReadLine();
            if (input.ToLower().Equals("y"))
            {
                cts.Cancel();
            }

            Console.WriteLine();
        }

        static IList<int> SampleData()
        {
            const int arraySize = 50000000;
            var r = new Random();
            return Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
        }
    }
}
