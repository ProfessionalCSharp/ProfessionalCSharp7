using BookServiceClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookServiceClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client app, wait for service");
            Console.ReadLine();
            ConfigureServices();
            var test = ApplicationServices.GetRequiredService<SampleRequest>();
            
            await test.ReadChaptersAsync();
            await test.ReadChapterAsync();
            await test.ReadNotExistingChapterAsync();
            await test.ReadXmlAsync();
            await test.AddChapterAsync();
            await test.UpdateChapterAsync();
            await test.RemoveChapterAsync();
            Console.ReadLine();
        }

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<UrlService>();
            services.AddSingleton<BookChapterClientService>();
            services.AddTransient<SampleRequest>();
            services.AddLogging(logger =>
            {
                logger.AddConsole();
            });

            ApplicationServices = services.BuildServiceProvider();
        }

        public static IServiceProvider ApplicationServices { get; private set; }
    }
}
