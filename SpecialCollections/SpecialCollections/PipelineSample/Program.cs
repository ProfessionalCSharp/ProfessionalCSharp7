using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PipelineSample
{
    class Program
    {
        static void Main()
        {
            StartPipelineAsync().Wait();
            Console.ReadLine();
        }

        public static async Task StartPipelineAsync()
        {
            var fileNames = new BlockingCollection<string>();
            var lines = new BlockingCollection<string>();
            var words = new ConcurrentDictionary<string, int>();
            var items = new BlockingCollection<Info>();
            var coloredItems = new BlockingCollection<Info>();
            Task t1 = PipelineStages.ReadFilenamesAsync(@"../..", fileNames);
            ColoredConsole.WriteLine("started stage 1");
            Task t2 = PipelineStages.LoadContentAsync(fileNames, lines);
            ColoredConsole.WriteLine("started stage 2");
            Task t3 = PipelineStages.ProcessContentAsync(lines, words);
            await Task.WhenAll(t1, t2, t3);
            ColoredConsole.WriteLine("stages 1, 2, 3 completed");
            Task t4 = PipelineStages.TransferContentAsync(words, items);
            Task t5 = PipelineStages.AddColorAsync(items, coloredItems);
            Task t6 = PipelineStages.ShowContentAsync(coloredItems);
            ColoredConsole.WriteLine("stages 4, 5, 6 started");

            await Task.WhenAll(t4, t5, t6);
            ColoredConsole.WriteLine("all stages finished");
        }
    }
}
