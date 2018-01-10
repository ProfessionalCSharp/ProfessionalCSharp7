using BooksAppX.Services;
using BooksAppX.ViewModels;
using BooksLib.Models;
using BooksLib.Services;
using BooksLib.ViewModels;
using Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace BooksAppX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterServices();

            MainPage = new BooksAppX.Views.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void RegisterServices()
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
            AppServices = services.BuildServiceProvider();
        }

        public IServiceProvider AppServices { get; private set; }
    }
}
