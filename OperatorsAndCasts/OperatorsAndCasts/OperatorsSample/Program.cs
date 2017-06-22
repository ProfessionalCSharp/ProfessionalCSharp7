using System;

namespace OperatorsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            PrefixAndPostfix();
            ConditionalOperator();
            try
            {
                OverflowExceptionSample();
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }

            IsOperator();
            IsOperatorWithConstPattern();
            IsOperatorWithTypePattern();
            AsOperatorSample();
            SizeofSample();
        }

        public void ShowPerson(Person p)
        {
            string firstName = p?.FirstName;
            int? age = p?.Age;
            int age1 = p?.Age ?? 0;
        }

        public static void UsePerson()
        {
            Person p = GetPerson();

            string city = p?.WorkAddress?.City;

        }

        public static Person GetPerson() =>
            new Person("Christian", "Nagel", 52, new Address("Austria", "Vienna"));

        private static void SizeofSample()
        {
            Console.WriteLine(nameof(SizeofSample));
            Console.WriteLine(sizeof(int));

            unsafe
            {
                Console.WriteLine(sizeof(Point));
            }
            Console.WriteLine();
        }

        private static void AsOperatorSample()
        {
            Console.WriteLine(nameof(AsOperatorSample));
            object o1 = "Some String";
            object o2 = 5;
            string s1 = o1 as string; // s1 = "Some String"
            string s2 = o2 as string; // s2 = null
            Console.WriteLine($"o1 as string assigns a string to s1: {s1}");
            Console.WriteLine($"o2 as string assigns null to s2 because o2 is not a string: {s2}");
            Console.WriteLine();
        }

        private static void IsOperatorWithTypePattern()
        {
            AMethodUsingPatternMatching(new Person("Katharina", "Nagel"));
        }

        public static void AMethodUsingPatternMatching(object o)
        {
            if (o is Person p)
            {
                Console.WriteLine($"o is a Person with firstname {p.FirstName}");
            }
        }


        private static void IsOperatorWithConstPattern()
        {
            Console.WriteLine(nameof(IsOperatorWithConstPattern));
            int i = 42;
            if (i is 42)
            {
                Console.WriteLine("i has the value 42");
            }

            object o = null;
            if (o is null)
            {
                Console.WriteLine("o is null");
            }
            Console.WriteLine();

        }

        private static void IsOperator()
        {
            Console.WriteLine(nameof(IsOperator));
            int i = 10;
            if (i is object) // always an object
            {
                Console.WriteLine("i is an object");
            }
            Console.WriteLine();
        }

        private static void OverflowExceptionSample()
        {
            Console.WriteLine(nameof(OverflowException));
            byte b = 255;
            checked
            {
                b++;
            }
            Console.WriteLine(b);
            Console.WriteLine();
        }

        private static void ConditionalOperator()
        {
            Console.WriteLine(nameof(ConditionalOperator));
            int x = 1;
            string s = x + " ";
            s += (x == 1 ? "man" : "men");
            Console.WriteLine(s);
            Console.WriteLine();
        }

        static void PrefixAndPostfix()
        {
            Console.WriteLine(nameof(PrefixAndPostfix));
            int x = 5;
            if (++x == 6) // true – x is incremented to 6 before the evaluation
            {
                Console.WriteLine("This will execute");
            }
            if (x++ == 7) // false – x is incremented to 7 after the evaluation
            {
                Console.WriteLine("This won’t");
            }
            Console.WriteLine();
        }
    }
}
