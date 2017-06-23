using System;

namespace BinaryCalculations
{
    class Program
    {
        static void Main()
        {
            SimpleCalculations();
            ShiftingBits();
            SignedNumbers();
        }

        private static void SignedNumbers()
        {
            Console.WriteLine("signed numbers");
            uint x1 = 0b1;
            Console.WriteLine(x1.ToBinaryString());
            Console.WriteLine($"number: {x1:X}");
            x1 = x1 << 31;
            Console.WriteLine("shift left 31 bits");
            Console.WriteLine(x1);
            Console.WriteLine(x1.ToBinaryString());
            Console.WriteLine($"number: {x1:X}");
            int x2 = (int)x1;
            Console.WriteLine(x2);
            Console.WriteLine(x2.ToBinaryString());
            Console.WriteLine($"number: {x2:X}");
            Console.WriteLine();

            int x3 = 0b1000;
            Console.WriteLine(x3);
            int x4 = ~x3;
            Console.WriteLine(x4);
            uint x5 = 0b11111111_11111111_11111111_11111111;
            Console.WriteLine($"x5: decimal: {x5} hex: {x5:X}");
            int x6 = (int)x5;
            Console.WriteLine($"x6: decimal: {x6} hex: {x6:X}");
            x6--;
            Console.WriteLine($"x6: decimal: {x6} hex: {x6:X}");
        }

        static void SimpleCalculations()
        {
            uint binary1 = 0b1111_0000_1100_0011_1110_0001_0001_1000;
            uint binary2 = 0b0000_1111_1100_0011_0101_1010_1110_0111;
            uint binaryAnd = binary1 & binary2;
            DisplayBits("AND", binaryAnd, binary1, binary2);
            uint binaryOR = binary1 | binary2;
            DisplayBits("OR", binaryOR, binary1, binary2);
            uint binaryXOR = binary1 ^ binary2;
            DisplayBits("XOR", binaryXOR, binary1, binary2);
            uint reverse1 = ~binary1;
            DisplayBits("NOT", reverse1, binary1);
        }
        static void DisplayBits(string title, uint result, uint left, uint? right = null)
        {
            Console.WriteLine(title);
            Console.WriteLine(left.ToBinaryString().AddSeparators());
            if (right.HasValue)
            {
                Console.WriteLine(right.Value.ToBinaryString().AddSeparators());
            }
            Console.WriteLine(result.ToBinaryString().AddSeparators());
            Console.WriteLine();
        }

        static void ShiftingBits()
        {
            ushort s1 = 0b01;
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine($"{s1} hex: {s1:X}");
                s1 = (ushort)(s1 << 1);
            }
        }
    }
}
