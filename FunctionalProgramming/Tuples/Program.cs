using DataLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TuplesLib;

namespace Tuples
{
    class Program
    {
        static void Main()
        {
            IntroTuples();
            TupleDeconstruction();
            ReturningTuples();        
            BehindTheScenes();
            UseALibrary();
            Mutability();
            TupleCompatibility();
            TupleNames();

            TuplesWithLinkedList();
            TuplesWithLinq();
            JsonSerialization();

            Deconstruct();
        }

        private static void TupleNames()
        {
            var t1 = Divide(9, 4);
            Console.WriteLine($"{t1.result}, {t1.remainder}");

            (int res, int rem) t2 = Divide(11, 3);
            Console.WriteLine($"{t2.res}, {t2.rem}");

            var t3 = (res: t1.result, rem: t1.remainder);

            // use inferred names
            var t4 = (t1.result, t1.remainder);
            Console.WriteLine($"{t4.result}, {t4.remainder}");
        }

        private static void TuplesWithLinkedList()
        {
            Console.WriteLine(nameof(TuplesWithLinkedList));
            var list = new LinkedList<int>(Enumerable.Range(0, 10));

            int value;
            LinkedListNode<int> node = list.First;
            do
            {
                (value, node) = (node.Value, node.Next);
                Console.WriteLine(value);
            } while (node != null);
            Console.WriteLine();
        }

        private static void Deconstruct()
        {
            var p1 = new Person("Katharina", "Nagel");

            (var first, var last) = p1;
            Console.WriteLine($"{first} {last}");
        }

        static void DeconstructWithExtensionsMethods()
        {
            var racer = Formula1.GetChampions().Where(r => r.LastName == "Lauda").First();
            (string first, string last, _, _) = racer;
            Console.WriteLine($"{first} {last}");
        }

        private static void TuplesWithLinq()
        {
            Console.WriteLine(nameof(TuplesWithLinq));
            UsingAnonymousTypes();
            UsingTuples();
            Console.WriteLine();
        }

        static void UsingAnonymousTypes()
        {
            var racerNamesAndStarts = Formula1.GetChampions()
                .Where(r => r.Country == "Italy")
                .OrderByDescending(r => r.Wins)
                .Select(r => new
                {
                    r.LastName,
                    r.Starts
                });

            foreach (var r in racerNamesAndStarts)
            {
                Console.WriteLine($"{r.LastName}, starts: {r.Starts}");
            }
        }


        static void UsingTuples()
        {
            var racerNamesAndStarts = Formula1.GetChampions()
                .Where(r => r.Country == "Italy")
                .OrderByDescending(r => r.Wins)
                .Select(r => 
                (
                    r.LastName,
                    r.Starts
                ));

            foreach (var r in racerNamesAndStarts)
            {
                Console.WriteLine($"{r.LastName}, starts: {r.Starts}");
            }
        }

        private static void BehindTheScenes()
        {
            (string s, int i) t1 = ("magic", 42); // tuple literal
            Console.WriteLine($"{t1.s} {t1.i}");

            ValueTuple<string, int> t2 = ValueTuple.Create("magic", 42);
            Console.WriteLine($"{t2.Item1}, {t2.Item2}");
        }

        static void TupleCompatibility()
        {
            // convert Tuple to ValueTuple
            Tuple<string, int, bool, Person> t1 = Tuple.Create("a string", 42, true, new Person("Katharina", "Nagel"));
            Console.WriteLine($"old tuple - string: {t1.Item1}, number: {t1.Item2}, bool: {t1.Item3}, Person: {t1.Item4}");
            (string s, int i, bool b, Person p) t2 = t1.ToValueTuple();
            Console.WriteLine($"new tuple - string: {t2.s}, number: {t2.i}, bool: {t2.b}, Person: {t2.p}");

            (string s, int i, bool b, Person p) = t1; // Deconstruct
            Console.WriteLine($"new tuple - string: {s}, number: {i}, bool: {b}, Person {p}");

            // convert ValueTuple to Tuple
            Tuple<string, int, bool, Person> t3 = t2.ToTuple();
            Console.WriteLine($"old tuple - string: {t1.Item1}, number: {t1.Item2}, bool: {t1.Item3}, Person: {t1.Item4}");
        }

        static void Mutability()
        {
            // old tuple is a immutable reference type
            Tuple<string, int> t1 = Tuple.Create("old tuple", 42);
            // t1.Item1 = "new string"; // not possible with Tuple

            // new tuple is a mutable value type
            (string s, int i) t2 = ("new tuple", 42);
            t2.s = "new string";
            t2.i = 43;
            t2.i++;

            Console.WriteLine($"new string: {t2.s} int: {t2.i}");
        }

        private static void JsonSerialization()
        {
            (string s, int i) t1 = ("magic", 42);
            string json = JsonConvert.SerializeObject(t1);
            Console.WriteLine(json);

            (string s1, int i1) t2 = JsonConvert.DeserializeObject<(string, int)>(json);
            Console.WriteLine($"s: {t2.s1}, i: {t2.i1}");
        }

        private static void UseALibrary()
        {
            var t = SimpleMath.Divide(5, 3);
            Console.WriteLine($"result: {t.result}, remainder: {t.remainder}");
        }

        private static void IntroTuples()
        {
            (string s, int i, Person p) t = ("magic", 42, new Person("Matthias", "Nagel"));
            Console.WriteLine($"s: {t.s}, i: {t.i}, p: {t.p}");

            var t2 = ("magic", 42, new Person("Matthias", "Nagel"));
            Console.WriteLine($"string: {t2.Item1}, int: {t2.Item2}, person: {t2.Item3}");

            var t3 = (s: "magic", i: 42, p: new Person("Matthias", "Nagel"));
            Console.WriteLine($"s: {t3.s}, i: {t3.i}, p: {t3.p}");

            (string astring, int anumber, Person aperson) t4 = t3;
            Console.WriteLine($"s: {t4.astring}, i: {t4.anumber}, p: {t4.aperson}");
        }

        private static void TupleDeconstruction()
        {
            (string s, int i, Person p) = ("magic", 42, new Person("Stephanie", "Nagel"));
            Console.WriteLine($"s: {s}, i: {i}, p: {p}");
            (var s1, var i1, var p1) = ("magic", 42, new Person("Stephanie", "Nagel"));
            Console.WriteLine($"s: {s1}, i: {i1}, p: {p1}");

            string s2;
            int i2;
            Person p2;
            (s2, i2, p2) = ("magic", 42, new Person("Katharina", "Nagel"));
            Console.WriteLine($"s: {s2}, i: {i2}, p: {p2}");

            (string s3, _, _) = ("magic", 42, new Person("Katharina", "Nagel"));
            Console.WriteLine(s3);
        }

        private static void ReturningTuples()
        {
            (int result, int remainder) = Divide(7, 2);
            Console.WriteLine($"7 / 2 - result: {result}, remainder: {remainder}");
        }

        static (int result, int remainder) Divide(int dividend, int divisor)
        {
            int result = dividend / divisor;
            int remainder = dividend % divisor;
            return (result, remainder);
        }
    }
}
