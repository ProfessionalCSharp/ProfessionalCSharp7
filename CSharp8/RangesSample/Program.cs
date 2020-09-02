using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RangesSample
{
    class Program
    {
        static void Main(string[] args)
        {
            BufferWithOffsetAndCount();
            BufferWithSpan();
            BufferWithSpanAndRanges();
            IndexSample();
            RangesSample();
            RangeAndIndexwithStringArray();
            RangeAndIndexWithStrings();
            CustomCollection();

            Console.WriteLine();
        }

        private static void IndexSample()
        {
            int[] data = { 1, 2, 3, 4, 5, 6 };
            Index ix1 = 0;
            Index ix2 = ^1;
            Index ix3 = new Index(3, fromEnd: true);
            ShowIndices(ix1, ix2, ix3);
            Console.WriteLine();

            void ShowIndices(params Index[] indices)
            {
                foreach (var ix in indices)
                {
                    Console.WriteLine($"value: {ix.Value}, is from end: {ix.IsFromEnd}, offset: {ix.GetOffset(data.Length)}");
                    Console.WriteLine($"value of the array element: {data[ix]}");
                }
            }
        }

        private static void RangesSample()
        {
            int[] data = { 1, 2, 3, 4, 5, 6 };
            Range r1 = 0..2;
            Range r2 = ^4..^2;
            Range r3 = 3..;
            Range r4 = ..4;
            Range r5 = ..;
            Range r6 = Range.StartAt(4);

            ShowRanges(r1, r2, r3, r4, r5, r6);
            Console.WriteLine();

            void ShowRanges(params Range[] ranges)
            {
                foreach (var r in ranges)
                {
                    (var offset, var length) = r.GetOffsetAndLength(data.Length);
                    Console.WriteLine($"range start: {r.Start}, end: {r.End}, offset: {offset}, length: {length}, content: {string.Join(' ', data[r])}");
                }
            }
        }

        private static void BufferWithOffsetAndCount()
        {
            Console.WriteLine("buffer with offset and count");

            // fill the first 6 bytes of the buffer with initial data, then add the content of a file witout the BOM, annd leave the last 8 bytes of the buffer empty

            byte[] buffer = new byte[64];
            using Stream stream = File.Open("QuickFox.txt", FileMode.Open);

            int offset = 3;
            int read = stream.Read(buffer, offset, count: buffer.Length - 8 - offset);
            Console.WriteLine($"read {read} bytes");

            // create initial data for the first 6 bytes
            // override 3 bytes from the BOM with the files read
            byte init = 0x_42;
            for (int i = 0; i < 6; i++)
            {
                buffer[i] = init;
            }

            string s = Encoding.UTF8.GetString(buffer);
            Console.WriteLine(s);
            Console.WriteLine();
        }

        private static void BufferWithSpan()
        {
            Console.WriteLine("buffer with span");

            // fill the first 6 bytes of the buffer with initial data, then add the content of a file witout the BOM, annd leave the last 8 bytes of the buffer empty

            byte[] buffer = new byte[64];
            var bufferSpan = buffer.AsSpan();
            using Stream stream = File.Open("QuickFox.txt", FileMode.Open);

            var spanForFile = bufferSpan.Slice(3, bufferSpan.Length - 8 - 3);
            int read = stream.Read(spanForFile);
            Console.WriteLine($"read {read} bytes");

            // create initial data for the first 6 bytes
            // override 3 bytes from the BOM with the files read
            byte init = 0x_42;
            bufferSpan.Slice(0, 6).Fill(init);

            string s = Encoding.UTF8.GetString(buffer);
            Console.WriteLine(s);
            Console.WriteLine();
        }

        private static void BufferWithSpanAndRanges()
        {
            Console.WriteLine("buffer with span and ranges");

            // fill the first 6 bytes of the buffer with initial data, then add the content of a file witout the BOM, annd leave the last 8 bytes of the buffer empty

            byte[] buffer = new byte[64];
            var bufferSpan = buffer.AsSpan();
            using Stream stream = File.Open("QuickFox.txt", FileMode.Open);

            var spanForFile = bufferSpan[3..^8];
            int read = stream.Read(spanForFile);
            Console.WriteLine($"read {read} bytes");

            // create initial data for the first 6 bytes
            // override 3 bytes from the BOM with the files read
            byte init = 0x_42;
            bufferSpan[0..6].Fill(init);

            string s = Encoding.UTF8.GetString(buffer);
            Console.WriteLine(s);
            Console.WriteLine();
        }

        private static void RangeAndIndexwithStringArray()
        {
            string[] names = { "James", "Niki", "Jochen", "Juan", "Michael", "Sebastian", "Nino", "Lewis" };

            string lewis = names[^1]; // uses an index
            Console.WriteLine(lewis);
            foreach (var name in names[2..^2]) // uses a range
            {
                Console.WriteLine(name);
            }
        }

        private static void RangeAndIndexWithStrings()
        {
            string fox1 = "the quick brown fox jumped over the lazy dogs";
            string quick = fox1[4..9];  // incl. to excl.
            string dog = fox1[^4..^1];
            string brownfoxjumped = fox1[10..];
            string thequick = fox1[..9];
            string fox2 = fox1[..];

            Console.WriteLine($"character accessed with index: {fox1[^2]}");

            ShowStrings(
                (nameof(fox1), fox1), 
                (nameof(quick), quick), 
                (nameof(dog), dog), 
                (nameof(brownfoxjumped), brownfoxjumped),
                (nameof(thequick), thequick),
                (nameof(fox2), fox2));

            static void ShowStrings(params (string Name, string Value)[] vals)
            {
                Console.WriteLine(nameof(RangeAndIndexWithStrings));
                foreach (var s in vals)
                {
                    Console.WriteLine($"{s.Name}, {s.Value}");
                }
                Console.WriteLine();
            }
        }

        private static void CustomCollection()
        {
            Console.WriteLine("custom collection");
            var coll1 = new MyCollection();
            int element = coll1[^3];
            Console.WriteLine(element);

            var range = coll1[11..15];
            foreach (var item in range)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine("using List<T>");
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            int item1 = list1[^2];
            Console.WriteLine(item1);
            var list2 = list1.Slice(2..^1);
            foreach (var item in list2)
            {
                Console.WriteLine(item);
            }
        }

    }
}
