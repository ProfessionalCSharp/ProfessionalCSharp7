using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebHooksReceiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((context, config) =>
                //{
                //    config.SetBasePath(Directory.GetCurrentDirectory())
                //        .AddJsonFile("appsettings,json", optional: false)
                //        .AddEnvironmentVariables();

                //    var builtConfig = config.Build();
                //    config.AddAzureKeyVault(
                //        $"https://{builtConfig["Vault"]}.vault.azure.net/",
                //            builtConfig["ClientId"],
                //            builtConfig["ClientSecret"]);
                //})
                .UseStartup<Startup>()
                .Build();
    }
}
