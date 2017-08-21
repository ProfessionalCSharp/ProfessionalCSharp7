using DISampleLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterServices();
        }

        public void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IMessageService, WPFMessageService>();
            services.AddTransient<ShowMessageViewModel>();
            Container = services.BuildServiceProvider();
        }

        public IServiceProvider Container { get; private set; }
    }
}
