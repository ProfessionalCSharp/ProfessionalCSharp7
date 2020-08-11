using System;
using System.Collections.Generic;
using System.Linq;

namespace RangesSample
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] buffer = new byte[4096];
            Span<byte> span = buffer.AsSpan();
            var slice = span.Slice(0, 50);
            slice.Fill(42);

            // use range instead of slice           
            var slice2 = span[0..100];
            foreach (var b in slice2)
            {
                Console.WriteLine(b);
            }

            string fox1 = "the quick brown fox jumped over the lazy dogs";
            string quick = fox1[4..9];  // inkl. to excl.
            string dog = fox1[^4..^1];
            string brownfoxjumped = fox1[10..];
            string thequick = fox1[..9];
            string fox2 = fox1[..];         

            var range = 4..9;
            var index = ^3;

            string[] names = { "James", "Niki", "Jochen", "Juan", "Michael", "Sebastian", "Nino", "Lewis" };

            string lewis = names[^1];
            foreach (var name in names[2..^2])
            {
                Console.WriteLine(name);
            }

            var coll = new MyCollection();
            var slice1 = coll[20..^55];

            Console.WriteLine();
        }
    }
}
