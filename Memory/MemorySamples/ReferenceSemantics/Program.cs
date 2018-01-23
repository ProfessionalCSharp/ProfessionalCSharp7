using System;
using System.Linq;

namespace ReferenceSemantics
{
    class Program
    {
        static void Main()
        {
            UseMember();
            UseRefMember();
            UseReadonlyRefMember();
            UseMax();
            UseItemOfContainer();
            UseArrayOfContainer();
        }

        private static void UseItemOfContainer()
        {
            Console.WriteLine(nameof(UseItemOfContainer));
            var c = new Container(Enumerable.Range(0, 10).Select(x => x).ToArray());
            ref int item = ref c.GetItem(3);
            item = 33;
            c.ShowAll();
            Console.WriteLine();
        }

        private static void UseArrayOfContainer()
        {
            Console.WriteLine(nameof(UseArrayOfContainer));
            var c = new Container(Enumerable.Range(0, 10).Select(x => x).ToArray());
            ref int[] d1 = ref c.GetData();
            d1 = new int[] { 4, 5, 6 };
            c.ShowAll();
            Console.WriteLine();
        }

        static void UseMember()
        {
            Console.WriteLine(nameof(UseMember));
            var d = new Data(11);
            int n = d.GetNumber();
            n = 42;
            d.Show();
            Console.WriteLine();
        }

        static void UseRefMember()
        {
            Console.WriteLine(nameof(UseRefMember));
            var d = new Data(11);
            ref int n = ref d.GetNumber();
            n = 42;
            d.Show();

            ref readonly int n2 = ref d.GetNumber();
            // n2 = 42; not allowed, it's readonly!

            Console.WriteLine();
        }

        static void UseReadonlyRefMember()
        {
            Console.WriteLine(nameof(UseReadonlyRefMember));
            var d = new Data(11);
            int n = d.GetReadonlyNumber();  // create a copy
            n = 42;
            d.Show();

            // ref int n = d.GetReadonlyNumber(); // not allowed
            ref readonly int n2 = ref d.GetReadonlyNumber();
            // n2 = 42; // not allowed
            Console.WriteLine();
        }

        static void UseMax()
        {
            Console.WriteLine(nameof(UseMax));
            int x = 4, y = 5;
            ref int z = ref Max(ref x, ref y);
            Console.WriteLine($"{z} is the max of {x} and {y}");
            z = x + y;
            Console.WriteLine($"y after changing z: {y}");

            Console.WriteLine();
        }
        static ref int Max(ref int x, ref int y)
        {
            if (x > y) return ref x;
            else return ref y;
        }
    }
}
