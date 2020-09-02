using System;
using System.Linq;

namespace StaticLocalFunctionsSample
{
    class Program
    {
        static void Main()
        {
            string[] names = { "James", "Niki", "Jochen" };
            var jNames = names.Where2(n => n.StartsWith("J"));
            foreach (var name in jNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
