using System;

namespace Wrox.ProCSharp.Arrays
{
    class Program
    {
        static void Main()
        {
            SortNames();
            Console.WriteLine();
            Person[] persons = GetPersons();
            SortPersons(persons);
            Console.WriteLine();
            SortUsingPersonComparer(persons);
        }


        static void SortUsingPersonComparer(Person[] persons)
        {
            Array.Sort(persons,
                new PersonComparer(PersonCompareType.FirstName));

            foreach (Person p in persons)
            {
                Console.WriteLine(p);
            }
        }

        static Person[] GetPersons()
        {
            return new Person[] {
                new Person { FirstName="Damon", LastName="Hill" },
                new Person { FirstName="Niki", LastName="Lauda" },
                new Person { FirstName="Ayrton", LastName="Senna" },
                new Person { FirstName="Graham", LastName="Hill" }
             };
        }

        static void SortPersons(Person[] persons)
        {
            Array.Sort(persons);
            foreach (Person p in persons)
            {
                Console.WriteLine(p);
            }
        }

        static void SortNames()
        {
            string[] names = {
                   "Christina Aguilera",
                   "Shakira",
                   "Beyonce",
                   "Gwen Stefani"
                 };

            Array.Sort(names);

            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
