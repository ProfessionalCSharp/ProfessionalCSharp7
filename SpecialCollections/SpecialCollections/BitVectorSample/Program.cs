using System;
using System.Collections.Specialized;

namespace BitVectorSample
{
    class Program
    {
        static void Main()
        {
            // create a mask using the CreateMask method
            var bits1 = new BitVector32();
            int bit1 = BitVector32.CreateMask();
            int bit2 = BitVector32.CreateMask(bit1);
            int bit3 = BitVector32.CreateMask(bit2);
            int bit4 = BitVector32.CreateMask(bit3);
            int bit5 = BitVector32.CreateMask(bit4);

            bits1[bit1] = true;
            bits1[bit2] = false;
            bits1[bit3] = true;
            bits1[bit4] = true;
            bits1[bit5] = true;
            Console.WriteLine(bits1);

            // create a mask using an indexer
            bits1[0xab_cdef] = true;
            Console.WriteLine();

            int received = 0x79ab_cdef;

            BitVector32 bits2 = new BitVector32(received);
            Console.WriteLine(bits2);

            // sections: FF EEE DDD CCCC BBBBBBBB
            // AAAAAAAAAAAA
            BitVector32.Section sectionA = BitVector32.CreateSection(0xfff);
            BitVector32.Section sectionB = BitVector32.CreateSection(0xff, sectionA);
            BitVector32.Section sectionC = BitVector32.CreateSection(0xf, sectionB);
            BitVector32.Section sectionD = BitVector32.CreateSection(0x7, sectionC);
            BitVector32.Section sectionE = BitVector32.CreateSection(0x7, sectionD);
            BitVector32.Section sectionF = BitVector32.CreateSection(0x3, sectionE);

            Console.WriteLine($"Section A: {bits2[sectionA].ToBinaryString()}");
            Console.WriteLine($"Section B: {bits2[sectionB].ToBinaryString()}");
            Console.WriteLine($"Section C: {bits2[sectionC].ToBinaryString()}");
            Console.WriteLine($"Section D: {bits2[sectionD].ToBinaryString()}");
            Console.WriteLine($"Section E: {bits2[sectionE].ToBinaryString()}");
            Console.WriteLine($"Section F: {bits2[sectionF].ToBinaryString()}");

            Console.ReadLine();
        }
    }
}
