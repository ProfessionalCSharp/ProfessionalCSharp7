using DotnetStandardLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetFrameworkApp
{
    class Program
    {
        static void Main()
        {
            Wrapper.ConsoleMessage("Hello from the .NET Framework");
            Wrapper.ShowConsoleType();
            Wrapper.WindowsMessage("Hello from the .NET Framework");
        }
    }
}
