using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CustomConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupConfiguration(args);
            ReadConfiguration();
        }

        private static void ReadConfiguration()
        {
            string val1 = Configuration.GetSection("section1")["key1"];
            Console.WriteLine(val1);
            string val2 = Configuration.GetSection("section1")["key2"];
            Console.WriteLine(val2);
        }

        static void SetupConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args);
            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }
    }
}
