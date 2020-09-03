using System;
using System.Collections.Generic;
using System.Linq;
using static CalculationType;

var operations = new Dictionary<string, CalculationType>()
{
    { "add", Addition },
    { "sub", Subtraction },
    { "mul", Multiplication },
    { "div", Division }
};

(CalculationType calculationType, int[] numbers) = ParseInput(args);

int result = Operate(calculationType, numbers);

Console.WriteLine($"{calculationType} - result: {result}");

(CalculationType calculationType, int[] numbers) ParseInput(string[] args)
{
    if (args.Length <= 0)
    {
        Usage();
        Environment.Exit(1);
    }

    if (!operations.ContainsKey(args[0]))
    {
        Usage();
        Environment.Exit(1);
    }

    int[] numbers = null;
    try
    {
        numbers = args[1..].Select(s => int.Parse(s)).ToArray();
    }
    catch (OverflowException)
    {
        Console.WriteLine("At least one number was too large");
        Environment.Exit(1);
    }
    catch (FormatException)
    {
        Console.WriteLine("Please enter numbers");
        Environment.Exit(1);
    }
    return (operations[args[0]], numbers);
}

static void Usage()
{
    Console.WriteLine("TopLevelProgram [add|sub|mul|div] <numbers>");
    Console.WriteLine("e.g. TopLevelProgram add 3 5 7");
}

static int Operate(CalculationType calculationType, int[] data) =>
    calculationType switch
    {
        Addition => data.Sum(),
        Subtraction => data.Aggregate((x, y) => x - y),
        Multiplication => data.Aggregate((x, y) => x * y),
        Division => data.Aggregate((x, y) => x / y),
        _ => throw new InvalidOperationException("invalid calculation type")
    };

enum CalculationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
