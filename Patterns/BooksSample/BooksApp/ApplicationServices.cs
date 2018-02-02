using BooksApp.Services;
using BooksApp.ViewModels;
using BooksLib.Models;
using BooksLib.Services;
using BooksLib.ViewModels;
using Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BooksApp
{
    public class ApplicationServices
    {
        private ApplicationServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IBooksRepository, BooksSampleRepository>();
            services.AddSingleton<IItemsService<Book>, BooksService>();
            services.AddTransient<BooksViewModel>();
            services.AddTransient<BookDetailViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddSingleton<IMessageService, UWPMessageService>();
            services.AddSingleton<INavigationService, UWPNavigationService>();
            services.AddSingleton<UWPInitializeNavigationService>();
            services.AddLogging(builder =>
            {
#if DEBUG
                builder.AddDebug();
#endif
            });
            ServiceProvider = services.BuildServiceProvider();
        }

        private static ApplicationServices _instance;
        private static object _instanceLock = new object();
        private static ApplicationServices GetInstance()
        {
            lock (_instanceLock)
            {
                return _instance ?? (_instance = new ApplicationServices());
            }
        }
        public static ApplicationServices Instance => _instance ?? GetInstance();

        public IServiceProvider ServiceProvider { get; }
    }
}
