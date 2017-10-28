using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace DataProtectionSample
{
    class Program
    {
        private const string readOption = "-r";
        private const string writeOption = "-w";
        private static readonly string[] options = { readOption, writeOption };

        static void Main(string[] args)
        {
            if (args.Length != 2 || args.Intersect(options).Count() != 1)
            {
                ShowUsage();
                return;
            }
            string fileName = args[1];

            MySafe safe = SetupDataProtection();


            switch (args[0])
            {
                case writeOption:
                    Write(safe, fileName);
                    break;
                case readOption:
                    Read(safe, fileName);
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        public static MySafe SetupDataProtection()
        {
            var services = new ServiceCollection();
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("."))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(20))
                .ProtectKeysWithDpapi();
            services.AddTransient<MySafe>();
          
            IServiceProvider provider = services.BuildServiceProvider();
            return provider.GetService<MySafe>();
        }

        public static void Read(MySafe safe, string fileName)
        {
            string encrypted = File.ReadAllText(fileName);
            string decrypted = safe.Decrypt(encrypted);
            Console.WriteLine(decrypted);
        }

        public static void Write(MySafe safe, string fileName)
        {
            Console.WriteLine("enter content to write:");
            string content = Console.ReadLine();
            string encrypted = safe.Encrypt(content);
            File.WriteAllText(fileName, encrypted);
            Console.WriteLine($"content written to {fileName}");
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: DataProtectionSample options filename");
            Console.WriteLine("Options:");
            Console.WriteLine("\t-r Read");
            Console.WriteLine("\t-w Write");
            Console.WriteLine();
        }
    }
}
