using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternMatching
{
    class Program
    {
        static void Main()
        {
            var p1 = new Person("Katharina", "Nagel");
            var p2 = new Person("Matthias", "Nagel");
            var p3 = new Person("Stephanie", "Nagel");
            object[] data = { null, 42, "astring", p1, new Person[] { p2, p3 } };

            Console.WriteLine(nameof(IsOperator));
            foreach (var item in data)
            {
                IsOperator(item);
            }
            Console.WriteLine();

            Console.WriteLine(nameof(SwitchStatement));
            foreach (var item in data)
            {
                SwitchStatement(item);
            }
            Console.WriteLine();
        }

        static void IsOperator(object item)
        {
            // const pattern
            if (item is null)
            {
                Console.WriteLine("item is null");
            }

            if (item is 42)
            {
                Console.WriteLine("item is 42");
            }

            // type pattern
            if (item is int)
            {
                Console.WriteLine($"Item is of type int");
            }

            if (item is int i)
            {
                Console.WriteLine($"Item is of type int with a value {i}");
            }

            if (item is string s)
            {
                Console.WriteLine($"Item is a string: {s}");
            }

            if (item is Person p && p.FirstName.StartsWith("Ka"))
            {
                Console.WriteLine($"Item is a person: {p.FirstName} {p.LastName}");
            }

            if (item is IEnumerable<Person> people)
            {
                string names = string.Join(", ", people.Select(p1 => p1.FirstName).ToArray());
                Console.WriteLine($"it's a Person collection containing {names}");
            }

            // var pattern
            if (item is var every)
            {
                Console.WriteLine($"it's var of type {every?.GetType().Name ?? "null"} with the value {every ?? "nothing"}");
            }
        }

        static void SwitchStatement(object item)
        {
            switch (item)
            {
                case null:
                case 42:
                    Console.WriteLine("it's a const pattern");
                    break;
                case int i:
                    Console.WriteLine($"it's a type pattern with int: {i}");
                    break;
                case string s:
                    Console.WriteLine($"it's a type pattern with string: {s}");
                    break;
                case Person p when p.FirstName == "Katharina":
                    Console.WriteLine($"type pattern match with Person and when clause: {p}");
                    break;
                case Person p:
                    Console.WriteLine($"type pattern match with Person: {p}");
                    break;
                case var every:
                    Console.WriteLine($"var pattern match: {every?.GetType().Name}");
                    break;
                default:
            }
        }
    }
}
