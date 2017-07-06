using System;
using System.Collections;

namespace BitArraySample
{
    class Program
    {
        static void Main()
        {
            var bits1 = new BitArray(9);
            bits1.SetAll(true);
            bits1.Set(1, false);
            bits1[5] = false;
            bits1[7] = false;
            Console.Write("initialized: ");
            Console.WriteLine(bits1.GetBitsFormat());

            Console.Write("not ");
            Console.Write(bits1.GetBitsFormat());
            bits1.Not();
            Console.Write(" = ");
            Console.WriteLine(bits1.GetBitsFormat());

            var bits2 = new BitArray(bits1);
            bits2[0] = true;
            bits2[1] = false;
            bits2[4] = true;
            Console.Write($"{bits1.GetBitsFormat()} OR {bits2.GetBitsFormat()}");
            Console.Write(" = ");
            bits1.Or(bits2);
            Console.WriteLine(bits1.GetBitsFormat());

            Console.Write($"{bits2.GetBitsFormat()} AND {bits1.GetBitsFormat()}");
            Console.Write(" = ");
            bits2.And(bits1);
            Console.WriteLine(bits2.GetBitsFormat());

            Console.Write($"{bits1.GetBitsFormat()} XOR {bits2.GetBitsFormat()}");
            bits1.Xor(bits2);
            Console.Write(" = ");
            Console.WriteLine(bits1.GetBitsFormat());

            Console.ReadLine();
        }
    }
}
