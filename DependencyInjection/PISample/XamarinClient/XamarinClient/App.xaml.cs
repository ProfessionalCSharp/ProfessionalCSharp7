using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using DISampleLib;

namespace XamarinClient
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            RegisterServices();

            MainPage = new XamarinClient.MainPage();

		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IMessageService, XamarinMessageService>();
            services.AddTransient<ShowMessageViewModel>();
            Container = services.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }
    }
}
