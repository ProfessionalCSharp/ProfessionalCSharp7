using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace UserSecretsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args);

#if DEBUG
            configBuilder.AddUserSecrets("UserSecretsSample-Id");
#endif
            IConfigurationRoot configuration = configBuilder.Build();
            string secretValue1 = configuration["Secret1"];
            Console.WriteLine($"secret: {secretValue1}");

        }
    }
}
