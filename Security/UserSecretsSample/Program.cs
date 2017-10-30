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
            string notASecret1 = configuration["NotASecret"];
            Console.WriteLine($"not a secret: {notASecret1}");

            string secretValue1 = configuration["Secret1"];
            Console.WriteLine($"secret: {secretValue1}");
        }
    }
}
