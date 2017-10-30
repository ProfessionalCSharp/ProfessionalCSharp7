using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Reflection;
using static System.Console;

namespace Wrox.ProCSharp.Composition
{
    class Program
    {
        public ICalculator Calculator { get; set; }

        static void Main()
        {
            var p = new Program();
            p.Bootstrap();
            p.Run();
        }

        public void Bootstrap()
        {
            var conventions = new ConventionBuilder();
            conventions.ForTypesDerivedFrom<ICalculator>().Export<ICalculator>().Shared();
            conventions.ForType<Program>().ImportProperty<ICalculator>(p => p.Calculator);

            var configuration = new ContainerConfiguration()
                .WithDefaultConventions(conventions)
                .WithAssemblies(GetAssemblies("c:/addins"));

            using (CompositionHost host = configuration.CreateContainer())
            {
                host.SatisfyImports(this, conventions);
            }
        }

        public void Run()
        {
            CalculatorLoop();
        }

        private IEnumerable<Assembly> GetAssemblies(string path)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(path, "*.dll");
            var assemblies = new List<Assembly>();
            foreach (var file in files)
            {
                Assembly assembly = Assembly.LoadFile(file);
                assemblies.Add(assembly);
            }
            return assemblies;
        }

        public void CalculatorLoop()
        {
            var operations = Calculator.GetOperations();
            var operationsDict = new SortedList<string, IOperation>();
            foreach (var item in operations)
            {
                WriteLine($"Name: {item.Name}, number operands: {item.NumberOperands}");
                operationsDict.Add(item.Name, item);
            }
            WriteLine();
            string selectedOp = null;
            do
            {
                try
                {
                    Write("Operation? ");
                    selectedOp = ReadLine();
                    if (selectedOp.ToLower() == "exit" || !operationsDict.ContainsKey(selectedOp))
                        continue;
                    var operation = operationsDict[selectedOp];
                    double[] operands = new double[operation.NumberOperands];
                    for (int i = 0; i < operation.NumberOperands; i++)
                    {
                        Write($"\t operand {i + 1}? ");
                        string selectedOperand = ReadLine();
                        operands[i] = double.Parse(selectedOperand);
                    }
                    WriteLine("calling calculator");
                    double result = Calculator.Operate(operation, operands);
                    WriteLine($"result: {result}");
                }
                catch (FormatException ex)
                {
                    WriteLine(ex.Message);
                    WriteLine();
                    continue;
                }
            } while (selectedOp != "exit");
        }
    }
}


