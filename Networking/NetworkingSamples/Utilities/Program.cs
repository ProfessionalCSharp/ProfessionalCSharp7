using System;
using System.Linq;
using System.Net;

namespace Utilities
{
    class Program
    {
        private static Command<string>[] s_Commands;

        static void Main(string[] args)
        {
            s_Commands = SetupCommands();

            if (args == null || args.Length != 2 || !s_Commands.Select(c => c.Option).Contains(args[0]))
            {
                ShowUsage();
                return;
            }

            s_Commands.Single(c => c.Option == args[0]).Action(args[1]);
            Console.ReadLine();
        }

        private static Command<string>[] SetupCommands() =>
            new Command<string>[]
            {
                new Command<string>("uri", "\tShow the part of the URI, e.g. uri www.wrox.com", UriSample),
                new Command<string>("ipaddress", "\tShow the part of the IP address, e.g. ipaddress 234.56.78.9", IPAddressSample),
            };


        private static void ShowUsage()
        {
            Console.WriteLine("Usage: Utilities command\n");
            Console.WriteLine();
            Console.WriteLine("commands");
            foreach (var command in s_Commands)
            {
                Console.WriteLine($"{command.Option} {command.Text}");
            }
            Console.WriteLine();
        }

        public static void IPAddressSample(string ipAddressString)
        {           
            IPAddress address;
            if (!IPAddress.TryParse(ipAddressString, out address))
            {
                Console.WriteLine($"cannot parse {ipAddressString}");
                return;
            }

            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.WriteLine($"byte {i}: {bytes[i]:X}");
            }

            Console.WriteLine($"family: {address.AddressFamily}, map to ipv6: {address.MapToIPv6()}, map to ipv4: {address.MapToIPv4()}");
            Console.WriteLine($"IPv4 loopback address: {IPAddress.Loopback}");
            Console.WriteLine($"IPv6 loopback address: {IPAddress.IPv6Loopback}");
            Console.WriteLine($"IPv4 broadcast address: {IPAddress.Broadcast}");
            Console.WriteLine($"IPv4 any address: {IPAddress.Any}");
            Console.WriteLine($"IPv6 any address: {IPAddress.IPv6Any}");
        }

        public static void UriSample(string url)
        {
            var page = new Uri(url);
            Console.WriteLine($"scheme: {page.Scheme}");

            Console.WriteLine($"host: {page.Host}, type:  {page.HostNameType}, idn host: {page.IdnHost}");
            Console.WriteLine($"port: {page.Port}");
            Console.WriteLine($"path: {page.AbsolutePath}");
            Console.WriteLine($"query: {page.Query}");
            foreach (var segment in page.Segments)
            {
                Console.WriteLine($"segment: {segment}");
            }

            var builder = new UriBuilder();
            builder.Host = "www.cninnovation.com";
            builder.Port = 80;
            builder.Path = "training/MVC";
            Uri uri = builder.Uri;
            Console.WriteLine(uri);
        }
    }
}
