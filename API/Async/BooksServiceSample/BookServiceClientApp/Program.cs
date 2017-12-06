using BookServiceClientApp.Models;
using BookServiceClientApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookServiceClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client app, wait for service");
            Console.ReadLine();
            RegisterServices();
            var test = Container.GetRequiredService<SampleRequestClient>();
            
            await test.ReadChaptersAsync();
            await test.ReadChapterAsync();
            await test.ReadNotExistingChapterAsync();
            await test.ReadXmlAsync();
            await test.AddChapterAsync();
            await test.UpdateChapterAsync();
            await test.RemoveChapterAsync();
            Console.ReadLine();
        }

        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<UrlService>();
            services.AddSingleton<BookChapterClientService>();
            services.AddTransient<SampleRequestClient>();

            Container = services.BuildServiceProvider();
        }

        public static IServiceProvider Container { get; private set; }
    }
}
