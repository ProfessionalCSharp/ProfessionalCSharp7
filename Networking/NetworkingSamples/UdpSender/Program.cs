using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (!ParseCommandLine(args, out int port, out string hostname, out bool broadcast, out string groupAddress, out bool ipv6))
            {
                ShowUsage();
                Console.ReadLine();
                return;
            }
            IPEndPoint endpoint = await GetIPEndPointAsync(port, hostname, broadcast, groupAddress, ipv6);
            await SenderAsync(endpoint, broadcast, groupAddress);
            Console.WriteLine("Press return to exit...");
            Console.ReadLine();
        }

        private static string GetValueForKey(string[] args, string key)
        {
            int? nextIndex = args.Select((a, i) => new { Arg = a, Index = i }).SingleOrDefault(a => a.Arg == key)?.Index + 1;
            if (!nextIndex.HasValue)
            {
                return null;
            }
            return args[nextIndex.Value];
        }

        private static bool ParseCommandLine(string[] args, out int port, out string hostname, out bool broadcast, out string groupAddress, out bool ipv6)
        {
            port = 0;
            hostname = string.Empty;
            broadcast = false;
            groupAddress = string.Empty;
            ipv6 = false;
            if (args.Length < 2 || args.Length > 5)
            {
                return false;
            }
            if (args.SingleOrDefault(a => a == "-p") == null)
            {
                Console.WriteLine("-p required");
                return false;
            }
            string[] requiredOneOf = { "-h", "-b", "-g" };
            if (args.Intersect(requiredOneOf).Count() != 1)
            {
                Console.WriteLine("either one (and only one) of -h -b -g required");
                return false;
            }

            // get port number
            string port1 = GetValueForKey(args, "-p");
            if (port1 == null || !int.TryParse(port1, out port))
            {
                return false;
            }

            // get optional host name
            hostname = GetValueForKey(args, "-h");

            broadcast = args.Contains("-b");

            ipv6 = args.Contains("-ipv6");

            // get optional group address
            groupAddress = GetValueForKey(args, "-g");
            return true;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: UdpSender -p port [-g groupaddress | -b | -h hostname] [-ipv6]");
            Console.WriteLine("\t-p port number\tEnter a port number for the sender");
            Console.WriteLine("\t-g group address\tGroup address in the range 224.0.0.0 to 239.255.255.255");
            Console.WriteLine("\t-b\tFor a broadcast");
            Console.WriteLine("\t-h hostname\tUse the hostname option if the message should be sent to a single host");
        }

        public static async Task<IPEndPoint> GetIPEndPointAsync(int port, string hostName, bool broadcast, string groupAddress, bool ipv6)
        {
            IPEndPoint endpoint = null;
            try
            {
                if (broadcast)
                {
                    endpoint = new IPEndPoint(IPAddress.Broadcast, port);
                }
                else if (hostName != null)
                {
                    IPHostEntry hostEntry = await Dns.GetHostEntryAsync(hostName);
                    IPAddress address = null;
                    if (ipv6)
                    {
                        address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetworkV6).FirstOrDefault();
                    }
                    else
                    {
                        address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
                    }

                    if (address == null)
                    {
                        Func<string> ipversion = () => ipv6 ? "IPv6" : "IPv4";
                        Console.WriteLine($"no {ipversion()} address for {hostName}");
                        return null;
                    }
                    endpoint = new IPEndPoint(address, port);
                }
                else if (groupAddress != null)
                {
                    endpoint = new IPEndPoint(IPAddress.Parse(groupAddress), port);
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(hostName)}, {nameof(broadcast)}, or {nameof(groupAddress)} must be set");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return endpoint;
        }

        private static async Task SenderAsync(IPEndPoint endpoint, bool broadcast, string groupAddress)
        {
            try
            {
                string localhost = Dns.GetHostName();
                using (var client = new UdpClient())
                {
                    client.EnableBroadcast = broadcast;
                    if (groupAddress != null)
                    {
                        client.JoinMulticastGroup(IPAddress.Parse(groupAddress));
                    }

                    bool completed = false;
                    do
                    {
                        Console.WriteLine("Enter a message or bye to exit");
                        string input = Console.ReadLine();
                        Console.WriteLine();
                        completed = input == "bye";

                        byte[] datagram = Encoding.UTF8.GetBytes($"{input} from {localhost}");
                        int sent = await client.SendAsync(datagram, datagram.Length, endpoint);
                    } while (!completed);

                    if (groupAddress != null)
                    {
                        client.DropMulticastGroup(IPAddress.Parse(groupAddress));
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}