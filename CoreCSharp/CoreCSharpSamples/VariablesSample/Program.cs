using System;

namespace VariablesSample
{
    class Program
    {
        static void Main()
        {
            var name = "Bugs Bunny";
            var age = 25;
            var isRabbit = true;
            Type nameType = name.GetType();
            Type ageType = age.GetType();
            Type isRabbitType = isRabbit.GetType();
            Console.WriteLine($"name is of type {nameType}");
            Console.WriteLine($"age is of type {ageType}");
            Console.WriteLine($"isRabbit is of type {isRabbitType}");
        }
    }
}
