using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;


namespace Wrox.ProCSharp.Composition
{
    public class Program
    {
        [Import]
        public ICalculator Calculator { get; set; }

        static void Main()
        {
            var p = new Program();
            p.Bootstrapper();
            p.Run();
        }

        public void Bootstrapper()
        {
            var configuration = new ContainerConfiguration()
                .WithPart<Calculator>();
            using (CompositionHost host = configuration.CreateContainer())
            {
                // Calculator = host.GetExport<ICalculator>();
                host.SatisfyImports(this);
            }
        }

        public void Run()
        {
            var operations = Calculator.GetOperations();
            var operationsDict = new SortedList<string, IOperation>();
            foreach (var item in operations)
            {
                Console.WriteLine($"Name: {item.Name}, number operands: {item.NumberOperands}");
                operationsDict.Add(item.Name, item);
            }
            Console.WriteLine();
            string selectedOp = null;
            do
            {
                try
                {
                    Console.Write("Operation? ");
                    selectedOp = Console.ReadLine();
                    if (selectedOp.ToLower() == "exit" || !operationsDict.ContainsKey(selectedOp))
                        continue;
                    var operation = operationsDict[selectedOp];
                    double[] operands = new double[operation.NumberOperands];
                    for (int i = 0; i < operation.NumberOperands; i++)
                    {
                        Console.Write($"\t operand {i + 1}? ");
                        string selectedOperand = Console.ReadLine();
                        operands[i] = double.Parse(selectedOperand);
                    }
                    Console.WriteLine("calling calculator");
                    double result = Calculator.Operate(operation, operands);
                    Console.WriteLine("result: {0}", result);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    continue;
                }
            } while (selectedOp != "exit");
        }
    }
}