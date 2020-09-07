using System;
using System.Runtime.CompilerServices;

namespace ModuleInitializerLib
{
    internal class TheInitializer
    {
        [ModuleInitializer()]
        public static void Init()
        {
            Console.WriteLine("Init!");
        }
    }
}
