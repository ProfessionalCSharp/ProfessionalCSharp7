using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading.Tasks;

namespace LoggingSample
{
    class Program
    {
        private static string s_url = "https://csharp.christiannagel.com";

        static async Task Main(string[] args)
        {
            if (args.Length == 1)
            {
                s_url = args[0];
            }

            RegisterServices();
            await RunSampleAsync();
            Console.WriteLine("Completed");
            Console.ReadLine();
        }

        static async Task RunSampleAsync()
        {
            var controller = AppServices.GetService<SampleController>();
            await controller.NetworkRequestSampleAsync(s_url);
        }

        static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddEventSourceLogger();
                builder.AddConsole();
#if DEBUG
                builder.AddDebug();
#endif
               // builder.AddFilter<ConsoleLoggerProvider>("LoggingSample", LogLevel.Error);
                builder.AddFilter<ConsoleLoggerProvider>((category, logLevel) =>
                {
                    if (category.Contains("SampleController") && logLevel >= LogLevel.Information) return true;
                    else if (logLevel >= LogLevel.Error) return true;
                    else return false;
                });
            });
            services.AddScoped<SampleController>();
            AppServices = services.BuildServiceProvider();
        }
         
        public static IServiceProvider AppServices { get; private set; }
    }
}
