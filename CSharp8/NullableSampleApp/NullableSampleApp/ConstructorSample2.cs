using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NullableSampleApp
{
    public class AppServices
    {
        public AppServices()
        {
            Services = RegisterServices();
        }

        public IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            //...
            return services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; }
    }

}
