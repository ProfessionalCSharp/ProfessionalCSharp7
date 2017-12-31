using System;
using BooksAppX.Services;
using BooksAppX.Views;
using BooksLib.Services;
using BooksLib.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace BooksAppX
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();
            RegisterServices();

            MainPage = new MainPage();
        }

		protected override void OnStart ()
		{

		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IBooksRepository, BooksSampleRepository>();
            services.AddSingleton<IBooksService, BooksService>();
            services.AddTransient<ManageBooksViewModel>();
            services.AddSingleton<IMessageService, XamarinMessageService>();
            services.AddLogging();
//            builder =>
//            {
//#if DEBUG
//                builder.AddDebug();
//#endif
//            });
            AppServices = services.BuildServiceProvider();
        }

        public IServiceProvider AppServices { get; private set; }
	}
}
