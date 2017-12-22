using System;

namespace OutKeywordSample
{
    class Program
    {
        static void Main()
        {
            int? x = 3;
            int x1 = x.HasValue ? x.Value : -1;
            int x2 = x ?? -1;

            
            // version 1
            string input1 = Console.ReadLine();
            int result1 = int.Parse(input1);
            Console.WriteLine($"result: {result1}");

            // version 2
            string input2 = Console.ReadLine();
            if (int.TryParse(input2, out int result2))
            {
                Console.WriteLine($"result: {result2}");
            }
            else
            {
                Console.WriteLine("not a number");
            }

            // version 3
            string input3 = Console.ReadLine();
            Console.WriteLine(GetParseResult(input3));
            Console.ReadLine();
        }

        static string GetParseResult(string input) =>
            int.TryParse(input, out int result) ? $"n: {result}" : "not a number"; 
    }
}
