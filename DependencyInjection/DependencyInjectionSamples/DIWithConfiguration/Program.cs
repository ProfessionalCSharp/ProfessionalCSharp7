using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace DIWithConfiguration
{
    class Program
    {
        static void Main()
        {
            DefineConfiguration();
            RegisterServices();
            var controller = Container.GetService<HomeController>();
            string result = controller.Hello("Katharina");
            Console.WriteLine(result);
        }

        static void DefineConfiguration()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
            Configuration = configBuilder.Build();
        }

        static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddSingleton<IGreetingService, GreetingService>();
            services.AddGreetingService(Configuration.GetSection("GreetingService"));
            services.AddTransient<HomeController>();
            Container = services.BuildServiceProvider();
        }
        public static IConfiguration Configuration { get; set; }
        public static IServiceProvider Container { get; private set; }
    }
}
