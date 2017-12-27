using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading.Tasks;

namespace LoggingScopeSample
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
                builder.AddConsole(options => options.IncludeScopes = true);
                builder.AddDebug();
            });
            services.AddScoped<SampleController>();
            AppServices = services.BuildServiceProvider();
        }

        public static IServiceProvider AppServices { get; private set; }
    }
}
