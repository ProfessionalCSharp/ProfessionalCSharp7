using System;

namespace Wrox
{
    class Program
    {
        static void Main()
        {
            // Try calling some static functions.
            Console.WriteLine($"Pi is {Math.GetPi()}");
            int x = Math.GetSquareOf(5);
            Console.WriteLine($"Square of 5 is {x}");

            // Instantiate a Math object
            var math = new Math();   // instantiate a reference type

            // Call instance members
            math.Value = 30;
            Console.WriteLine($"Value field of math variable contains {math.Value}");
            Console.WriteLine($"Square of 30 is {math.GetSquare()}");

            Console.ReadLine();
        }
    }
}
