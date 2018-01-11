using System;

namespace ReadonlyStructSample
{
    class Program
    {
        static void Main()
        {
            var point = new Dimensions(3, 6);
            Foo(point);

            Console.ReadLine();
        }

        public static void Foo(Dimensions dimensions)
        {
            
        }
    }
}
