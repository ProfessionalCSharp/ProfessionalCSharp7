using System;

namespace Native
{
    class Program
    {
        static void Foo()
        {
            nint x = 11;
            Console.WriteLine(x);
        }

        unsafe static void Main()
        {
            nint ni = 42;
            nuint nui = 42;
            Console.WriteLine($"{ni} {nui}");
            Console.WriteLine($"nint: {sizeof(nint)} bytes");

            Console.WriteLine($"nuint: {sizeof(nuint)} bytes");

            Calc(&Add);
            Calc(&Subtract);
        }

        unsafe public static void Calc(delegate*<int, int, int> func)
        {
            int result = func(42, 11);
            Console.WriteLine($"function pointer result: {result}");
        }

        public static int Add(int x, int y) => x + y;
        public static int Subtract(int x, int y) => x - y;
    }
}
