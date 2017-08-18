using Microsoft.Extensions.DependencyInjection;
using System;

namespace ReplacingServices
{
    class Program
    {
        static void Main()
        {
            RegisterServices();
            var controller = Container.GetService<HomeController>();
            string result = controller.Hello("Katharina");

            ReplaceService();
            Console.WriteLine(result);
        }

        private static void ReplaceService() => throw new NotImplementedException();

        static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IGreetingService, GreetingService>();
            services.AddTransient<HomeController>();
            Container = services.BuildServiceProvider();
        }
        public static IServiceProvider Container { get; private set; }
    }
}
