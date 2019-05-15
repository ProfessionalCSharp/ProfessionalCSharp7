using System;
using System.Collections.Generic;

namespace RangesSample
{
    class Program
    {
        static void Main()
        {
            TheHatOperator();
            RangeWithStrings();
            RangesWithArrays();
        }

        private static void TheHatOperator()
        {
            var last = ^1;
            int[] arr = { 1, 2, 3 };
            int lastItem = arr[last];
            Console.WriteLine(lastItem);

            int lastItem2 = arr[arr.Length - 1];
            Console.WriteLine(lastItem2);
        }

        private static void RangeWithStrings()
        {
            string text1 = "the quick brown fox jumped over the lazy dogs";
            string lazyDogs1 = text1.Substring(36, 9);

            string lazyDogs2 = text1.Substring(text1.Length - 9, 9);

            string lazyDogs3 = text1[^9..^0];
            var start = ^9;

            var end = ^0;

            var range = new Range(start, end);
            // string lazyDogs4 = text1.Substring(range);

            string lazyDogs5 = text1[^9..];  // Range.FromStart
            Console.WriteLine($"{lazyDogs5} ^9..");

            string lazyDogs6 = text1[36..]; // Range.FromStart
            Console.WriteLine($"{lazyDogs6} 36..");

            string thequick = text1[..9]; // Range.ToEnd
            Console.WriteLine($"{thequick} ..9");

            string completeString = text1[..]; // Range.All
            Console.WriteLine($"{completeString} ..");
        }

        private static void RangesWithArrays()
        {
            var span1 = new[] { 1, 4, 8, 11, 19, 31 }.AsSpan();
            
            var range = 2..5;
            var slice = span1[range];
           
            foreach (var item in slice)
            {
                Console.WriteLine(item);
            }
        }
    }
}
