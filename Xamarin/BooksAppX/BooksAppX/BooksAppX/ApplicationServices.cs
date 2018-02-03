using BooksAppX.Services;
using BooksAppX.ViewModels;
using BooksLib.Models;
using BooksLib.Services;
using BooksLib.ViewModels;
using Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BooksAppX
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
            services.AddTransient<AboutViewModel>();
            services.AddSingleton<IMessageService, XamarinMessageService>();
            services.AddSingleton<INavigationService, XamarinNavigationService>();
            services.AddSingleton<XamarinInitializeNavigationService>();
            services.AddLogging();
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
