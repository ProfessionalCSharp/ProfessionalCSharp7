using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading.Tasks;

namespace LoggingSample
{
    class Program
    {
        private static bool s_useScope = false;
        private static string s_url = "https://csharp.christiannagel.com";

        static async Task Main(string[] args)
        {
            if (args.Length == 2)
            {
                if (args[0] == "--usescope")
                    s_useScope = true;
                s_url = args[1];
            }
            else if (args.Length == 1)
            {
                s_url = args[0];
            }

            RegisterServices();
            if (s_useScope)
            {
                await RunSampleScopeAsync();
            }
            else
            {
                await RunSampleAsync();
            }
            Console.ReadLine();
        }

        static async Task RunSampleAsync()
        {
            var controller = AppServices.GetService<SampleController>();
            await controller.NetworkRequestSampleAsync(s_url);
        }

        static async Task RunSampleScopeAsync()
        {
            var controller = AppServices.GetService<SampleController>();
            await controller.NetworkRequestSampleScopeAsync(s_url);
        }

        static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(options =>
            {
                options.AddEventSourceLogger();
                if (s_useScope)
                {
                    options.AddConsole(consoleOptions => consoleOptions.IncludeScopes = true);
                }
                else
                {
                    options.AddConsole();
                }
                options.AddDebug();
              //  options.AddFilter<ConsoleLoggerProvider>(level => level >= LogLevel.Error);
            });
            services.AddScoped<SampleController>();
            AppServices = services.BuildServiceProvider();
        }
         
        public static IServiceProvider AppServices { get; private set; }
    }
}
