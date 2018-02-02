using Microsoft.Extensions.DependencyInjection;
using System;
using WindowsAppChatClient.Services;
using WindowsAppChatClient.ViewModels;

namespace WindowsAppChatClient
{
    public class ApplicationServices
    {
        private ApplicationServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<ChatViewModel>();
            services.AddTransient<GroupChatViewModel>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<UrlService>();
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
