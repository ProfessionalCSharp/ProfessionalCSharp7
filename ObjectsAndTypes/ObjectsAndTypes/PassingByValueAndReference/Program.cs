using System.Runtime.CompilerServices;
using System;

namespace PassingByValueAndReference
{
    public class A
    {
        public int X { get; set; }
    }

    class Program
    {
        public static void ChangeA(A a)
        {
            a.X = 2;
        }

        static void Main()
        {
            A a1 = new A { X = 1 };
            ChangeA(a1);
            Console.WriteLine($"a1.X: {a1.X}");
            Console.ReadLine();
        }
    }
}
