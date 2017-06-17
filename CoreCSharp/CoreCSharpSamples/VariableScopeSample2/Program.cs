using System;

namespace VariableScopeSample2
{
    class Program
    {
        static int Main(string[] args)
        {
            int j = 20;
            for (int i = 0; i < 10; i++)
            {
                // int j = 30; // Can't do this — j is still in scope
                Console.WriteLine(j + i);
            }
            return 0;
        }
    }
}
