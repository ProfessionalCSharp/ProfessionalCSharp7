using System;

namespace VariableScopeSample
{
    class Program
    {
        private int j;

        static int Main()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }  // i goes out of scope here
               // We can declare a variable named i again, because
               // there's no other variable with that name in scope
            for (int i = 9; i >= 0; i--)
            {
                Console.WriteLine(i);
            }  // i goes out of scope here.
            return 0;
        }
    }
}
