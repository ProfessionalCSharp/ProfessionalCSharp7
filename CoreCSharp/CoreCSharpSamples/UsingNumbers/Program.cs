using System;

namespace UsingNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            DigitSeparators();
            BinaryNumbers();
        }

        static void DigitSeparators()
        {
            long l1 = 0x123_4567_89ab_cedf;
            long l2 = 0x123456789abcedf;
            long l3 = 0x12345_6789_abc_ed_f;
            Console.WriteLine($"{l1:X}");
            Console.WriteLine($"{l2:X}");
            Console.WriteLine($"{l3:X}");
            Console.WriteLine();
        }

        static void BinaryNumbers()
        {
            uint binary1 = 0b1111_1110_1101_1100_1011_1010_1001_1000;
            uint hex1 = 0xfedcba98; 
            uint binary2 = 0b111_110_101_100_011_010_001_000;
            ushort binary3 = 0b1111_0000_101010_11;
            Console.WriteLine($"{binary1:X}");
            Console.WriteLine($"{hex1:X}");
            Console.WriteLine($"{binary2:X}");
            Console.WriteLine($"{binary3:X}");
        }
    }
}
