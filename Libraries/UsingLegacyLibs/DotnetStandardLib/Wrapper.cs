using DotnetFrameworkLib;
using System;

namespace DotnetStandardLib
{
    public class Wrapper
    {
        public static void ConsoleMessage(string message) => Legacy.ConsoleMessage(message);

        public static void WindowsMessage(string message) => Legacy.WindowsMessage(message);

        public static void ShowConsoleType() => Legacy.ShowConsoleType();
    }
}
