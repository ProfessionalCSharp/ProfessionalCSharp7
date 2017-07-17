using System;
using System.Runtime.CompilerServices;

namespace LocalFunctions
{
    class Program
    {
        static void Main()
        {
            IntroWithLambdaExpression();
            IntroWithLocalFunctions();
            IntroWithLocalFunctionsWithExpressionBodies();
            IntroWithLocalFunctionsWithClosures();
            YieldSampleSimple();
            YieldSampleWithPrivateMethod();
            YieldSampleWithLocalFunction();
            SortSample();
            Algorithms.WhenDoesItEnd();
        }

        private static void IntroWithLambdaExpression()
        {
            Func<int, int, int> add = (x, y) =>
            {
                return x + y;
            };
            int result = add(37, 5);
            Console.WriteLine(result);
        }

        static void IntroWithLocalFunctions()
        {
            int add(int x, int y)
            {
                return x + y;
            }
            int result = add(37, 5);
            Console.WriteLine(result);
        }

        private static void IntroWithLocalFunctionsWithExpressionBodies()
        {
            int add(int x, int y) => x + y;

            int result = add(37, 5);
            Console.WriteLine(result);
        }

        private static void IntroWithLocalFunctionsWithClosures()
        {
            int z = 3;
            int result = add(37, 5);
            Console.WriteLine(result);

            int add(int x, int y) => x + y + z;
        }

        private static void YieldSampleSimple()
        {
#line 1000
            Console.WriteLine(nameof(YieldSampleSimple));
            try
            {
                string[] names = { "James", "Niki", "John", "Gerhard", "Jack" };
                var q = names.Where1(null);

                foreach (var n in q)  // callstack position for exception
                {
                    Console.WriteLine(n);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine();
        }

        private static void SortSample()
        {
            Console.WriteLine(nameof(SortSample));
            int[] elements = { 7, 3, 4, 9, 11, 99, 22, 3, 5, 7, 12, 14, 97, 33, 343 };
            Algorithms.QuickSort(elements);
            foreach (var item in elements)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        private static void YieldSampleWithPrivateMethod()
        {
#line 1000
            Console.WriteLine(nameof(YieldSampleWithPrivateMethod));
            try
            {
                string[] names = { "James", "Niki", "John", "Gerhard", "Jack" };
                var q = names.Where2(null);  // callstack position for exception

                foreach (var n in q)
                {
                    Console.WriteLine(n);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine();
        }

        private static void YieldSampleWithLocalFunction()
        {
#line 1000
            Console.WriteLine(nameof(YieldSampleWithLocalFunction));
            try
            {
                string[] names = { "James", "Niki", "John", "Gerhard", "Jack" };
                var q = names.Where3(null);  // callstack position for exception

                foreach (var n in q)
                {
                    Console.WriteLine(n);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine();
        }
    }
}
