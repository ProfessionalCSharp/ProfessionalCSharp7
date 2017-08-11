using System;
using System.Threading.Tasks;
#if WINDOWS_UWP
using Windows.UI.Popups;
#endif

namespace SharedProject
{
    internal class Message
    {
#if NETCOREAPP2_0
        public static void Show(string message)
        {
            Console.WriteLine(message);
        }
#elif WINDOWS_UWP
        public static async Task ShowAsync(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }
#endif

        public static int Add(int x, int y) => x + y;
    }
}
