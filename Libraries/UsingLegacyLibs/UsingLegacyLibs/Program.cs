using DotnetStandardLib;
using System;
using System.IO;

namespace UsingLegacyLibs
{
    class Program
    {
        static void Main()
        {
            Wrapper.ConsoleMessage("Hello from .NET Core");
            Wrapper.ShowConsoleType();
            try
            {
                Wrapper.WindowsMessage("Hello from .NET Core");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
