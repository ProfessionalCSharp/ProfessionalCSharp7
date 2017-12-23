using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LoggingSample
{
    class Program
    {
        static async Task Main()
        {
            RegisterServices();
            await RunSampleAsync();
            Console.ReadLine();
        }

        static async Task RunSampleAsync()
        {
            var controller = AppServices.GetService<SampleController>();
            await controller.NetworkRequestSample2Async("https://csharp.christiannagel.com");
        }

        static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(options =>
            {
                options.AddEventSourceLogger();
                options.AddConsole();
                options.AddDebug().AddFilter(level => level >= LogLevel.Information);       
            });
            services.AddScoped<SampleController>();
            AppServices = services.BuildServiceProvider();
        }
         
        public static IServiceProvider AppServices { get; private set; }
    }
}
