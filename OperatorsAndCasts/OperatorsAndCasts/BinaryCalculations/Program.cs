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
            Console.WriteLine(nameof(SignedNumbers));

            void DisplayNumber(string title, int x) =>
                Console.WriteLine($"{title,-11} bin: {x.ToBinaryString().AddSeparators()}, dec: {x}, hex: {x:X}");

            int maxNumber = int.MaxValue;
            DisplayNumber("max int", maxNumber);
            for (int i = 0; i < 3; i++)
            {
                maxNumber++;
                DisplayNumber($"added {i + 1}", maxNumber);
            }
            Console.WriteLine();

            int zero = 0;
            DisplayNumber("zero", zero);
            for (int i = 0; i < 3; i++)
            {
                zero--;
                DisplayNumber($"subtracted {i + 1}", zero);
            }
            Console.WriteLine();

            int minNumber = int.MinValue;
            DisplayNumber("min number", minNumber);
            for (int i = 0; i < 3; i++)
            {
                minNumber++;
                DisplayNumber($"added {i + 1}", minNumber);
            }
            Console.WriteLine();
        }

        static void SimpleCalculations()
        {
            Console.WriteLine(nameof(SimpleCalculations));
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
            Console.WriteLine();
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
            Console.WriteLine(nameof(ShiftingBits));
            ushort s1 = 0b01;
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine($"{s1.ToBinaryString()} {s1} hex: {s1:X}");
                s1 = (ushort)(s1 << 1);
            }
            Console.WriteLine();
        }
    }
}
