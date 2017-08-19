using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIWithOptions
{
    public static class GreetingServiceExtensions
    {
        public static IServiceCollection AddGreetingService(this IServiceCollection collection,
            Action<GreetingServiceOptions> setupAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);
            return collection.AddTransient<IGreetingService, GreetingService>();
        }
    }
}
