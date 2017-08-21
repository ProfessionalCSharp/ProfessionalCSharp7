using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIWithAutofac

{
    class Program
    {
        static void Main()
        {
            using (IContainer container = RegisterServices())
            {
                var controller = container.Resolve<HomeController>();
                string result = controller.Hello("Katharina");
               
                Console.WriteLine(result);
            }
        }

        static IContainer RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IGreetingService, GreetingService>();
            services.AddTransient<HomeController>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            return builder.Build();
        }
    }
}
