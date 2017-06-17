using System;

namespace VariableScopeSample3
{
    class Program
    {
        static int j = 20;
        static int Main(string[] args)
        {
            int j = 30;
            Console.WriteLine(j);
            return 0;
        }
    }
}
