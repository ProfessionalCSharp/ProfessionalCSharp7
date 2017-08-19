using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIWithOptions
{
    class Program
    {
        static void Main()
        {
            using (var container = RegisterServices())
            {
                var controller = container.GetService<HomeController>();
                string result = controller.Hello("Katharina");
                Console.WriteLine(result);
            }
        }

        static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IGreetingService, GreetingService>();
            services.AddGreetingService(options =>
            {
                options.From = "Christian";
            });
            services.AddTransient<HomeController>();
            return services.BuildServiceProvider();
        }
    }
}
