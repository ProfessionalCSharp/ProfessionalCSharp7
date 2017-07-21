using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Reflection;

namespace ClientApp
{
    class Program
    {
        private const string CalculatorTypeName = "CalculatorLib.Calculator";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            UsingReflection(args[0]);
            UsingReflectionWithDynamic(args[0]);
        }

        private static void ShowUsage()
        {
            Console.WriteLine($"Usage: {nameof(ClientApp)} path");
            Console.WriteLine();
            Console.WriteLine("Copy CalculatorLib.dll to an addin directory");
            Console.WriteLine("and pass the absolute path of this directory when starting the application to load the library");
        }

        private static void UsingReflectionWithDynamic(string addinPath)
        {
            double x = 3;
            double y = 4;
            dynamic calc = GetCalculator(addinPath);
            double result = calc.Add(x, y);
            Console.WriteLine($"the result of {x} and {y} is {result}");

            try
            {
                result = calc.Multiply(x, y);
            }
            catch (RuntimeBinderException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void UsingReflection(string addinPath)
        {
            double x = 3;
            double y = 4;
            object calc = GetCalculator(addinPath);

            object result = calc.GetType().GetMethod("Add").Invoke(calc, new object[] { x, y });
            Console.WriteLine($"the result of {x} and {y} is {result}");
        }

        private static object GetCalculator(string addinPath)
        {
            Assembly assembly = Assembly.LoadFile(addinPath);
            return assembly.CreateInstance(CalculatorTypeName);
        }
    }
}
