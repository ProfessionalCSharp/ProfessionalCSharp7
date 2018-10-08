using System;
using System.Net;
using System.Threading.Tasks;

namespace DnsLookup
{
    class Program
    {
        static async Task Main()
        {
            do
            {
                Console.Write("Hostname:\t");
                string hostname = Console.ReadLine();
                if (hostname.CompareTo("exit") == 0)
                {
                    Console.WriteLine("bye!");
                    return;
                }
                await OnLookupAsync(hostname);
                Console.WriteLine();
            } while (true);
        }

        public static async Task OnLookupAsync(string hostname)
        {
            try
            {
                IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostname);

                Console.WriteLine($"Hostname: {ipHost.HostName}");

                // Aliases not populated by GetHostEntryAsync
                //if (ipHost.Aliases.Length != 0)
                //{
                //    Console.WriteLine($"Aliases: {string.Join(", ", ipHost.Aliases)}");
                //}

                foreach (IPAddress address in ipHost.AddressList)
                {
                    Console.WriteLine($"Address Family: {address.AddressFamily}");
                    Console.WriteLine($"Address: {address}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
