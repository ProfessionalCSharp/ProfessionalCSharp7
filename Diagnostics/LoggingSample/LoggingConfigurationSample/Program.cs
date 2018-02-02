using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LoggingConfigurationSample
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

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration configuration = configurationBuilder.Build();
            RegisterServices(configuration);
            await RunSampleAsync();
            Console.WriteLine("Completed");
            Console.ReadLine();
        }

        static async Task RunSampleAsync()
        {
            var controller = AppServices.GetService<SampleController>();
            await controller.NetworkRequestSampleAsync(s_url);
        }

        static void RegisterServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"))
                .AddConsole();
#if DEBUG
                builder.AddDebug();
#endif
            });
            services.AddScoped<SampleController>();
            AppServices = services.BuildServiceProvider();
        }

        public static IServiceProvider AppServices { get; private set; }
    }
}
