using System;
using System.Collections;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Arrays
{
    class Program
    {
        static void Main()
        {
            var janet = new Person(1, "Janet", "Jackson");
            Person[] persons1 = { new Person(2, "Michael", "Jackson"), janet };
            Person[] persons2 = { new Person(2, "Michael", "Jackson"), janet };
            if (persons1 != persons2)
            {
                Console.WriteLine("not the same reference");
            }

            if (!persons1.Equals(persons2))
            {
                Console.WriteLine("equals returns false - not the same reference");
            }

            if ((persons1 as IStructuralEquatable).Equals(persons2, EqualityComparer<Person>.Default))
            {
                Console.WriteLine("the same content");
            }
        }
    }
}
