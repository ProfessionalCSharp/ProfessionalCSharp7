using System;

namespace MulticastDelegates
{
    class MathOperations
    {
        public static void MultiplyByTwo(double value) =>
            Console.WriteLine($"Multiplying by 2: {value} gives {value * 2}");

        public static void Square(double value) =>
            Console.WriteLine($"Squaring: {value} gives {value * value}");
    }
}