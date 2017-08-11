using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotnetFrameworkLib
{
    public class Legacy
    {
        public static void ConsoleMessage(string message)
        {
            Console.WriteLine($"From the .NET Framework Lib: {message}");
        }

        public static void WindowsMessage(string message)
        {
            MessageBox.Show($"Windows Forms: {message}");
        }

        public static void ShowConsoleType()
        {
            Console.WriteLine($"The type {nameof(Console)} is from {Assembly.GetAssembly(typeof(Console)).FullName}");
        }
    }
}
