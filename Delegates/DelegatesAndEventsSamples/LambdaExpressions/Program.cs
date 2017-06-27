using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Delegates
{
    class Program
    {
        static void Main()
        {
            SimpleDemos();
            ClosureWithModification();
            ClosureWithForEach();
        }


        static void SimpleDemos()
        {
            Console.WriteLine(nameof(SimpleDemos));
            Func<string, string> oneParam = s => $"change uppercase {s.ToUpper()}";
            Console.WriteLine(oneParam("test"));

            Func<double, double, double> twoParams = (x, y) => x * y;
            Console.WriteLine(twoParams(3, 2));

            Func<double, double, double> twoParamsWithTypes = (double x, double y) => x * y;
            Console.WriteLine(twoParamsWithTypes(4, 2));

            Func<double, double> operations = x => x * 2;
            operations += x => x * x;

            ProcessAndDisplayNumber(operations, 2.0);
            ProcessAndDisplayNumber(operations, 7.94);
            ProcessAndDisplayNumber(operations, 1.414);
            Console.WriteLine();
        }

        static void ProcessAndDisplayNumber(Func<double, double> action, double value)
        {
            double result = action(value);
            Console.WriteLine($"Value is {value}, result of operation is {result}");
        }

        static void ClosureWithModification()
        {
            Console.WriteLine(nameof(ClosureWithModification));
            int someVal = 5;
            Func<int, int> f = x => x + someVal;

            someVal = 7;

            Console.WriteLine(f(3));
            Console.WriteLine();
        }

        static void ClosureWithForEach()
        {
            Console.WriteLine(nameof(ClosureWithForEach));
            var values = new List<int>() { 10, 20, 30 };
            var funcs = new List<Func<int>>();
            foreach (var val in values)
            {
                funcs.Add(() => val);
            }
            foreach (var f in funcs)
            {
                Console.WriteLine(f());
            }
            Console.WriteLine();
        }
    }
}
