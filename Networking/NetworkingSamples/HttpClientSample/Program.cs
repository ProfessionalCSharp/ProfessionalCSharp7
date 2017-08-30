using System;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientSample
{
    class Program
    {
        private static Command[] s_Commands;

        static async Task Main(string[] args)
        {
            var samples = new HttpClientSamples();
            s_Commands = SetupCommands(samples);

            if (args.Length == 0 || args.Length > 1 || !s_Commands.Select(c => c.Option).Contains(args[0]))
            {
                ShowUsage();
                return;
            }

            await s_Commands.Single(c => c.Option == args[0]).ActionAsync();
            Console.ReadLine();
        }

        private static Command[] SetupCommands(HttpClientSamples samples) =>
            new Command[]
            {
                new Command("-s", nameof(HttpClientSamples.GetDataSimpleAsync), samples.GetDataSimpleAsync),
                new Command("-a", nameof(HttpClientSamples.GetDataAdvancedAsync), samples.GetDataAdvancedAsync),
                new Command("-e", nameof(HttpClientSamples.GetDataWithExceptionsAsync), samples.GetDataWithExceptionsAsync),
                new Command("-h", nameof(HttpClientSamples.GetDataWithHeadersAsync), samples.GetDataWithHeadersAsync),
                new Command("-m", nameof(HttpClientSamples.GetDataWithMessageHandlerAsync), samples.GetDataWithMessageHandlerAsync),
            };

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: HttpClientSample [options]");
            Console.WriteLine();
            foreach (var command in s_Commands)
            {
                Console.WriteLine($"{command.Option} {command.Text}");
            }
        }
    }
}