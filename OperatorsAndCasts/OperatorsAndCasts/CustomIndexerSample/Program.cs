using System;

namespace CustomIndexerSample
{
    class Program
    {
        static void Main()
        {
            var p1 = new Person("Ayrton", "Senna", new DateTime(1960, 3, 21));
            var p2 = new Person("Ronnie", "Peterson", new DateTime(1944, 2, 14));
            var p3 = new Person("Jochen", "Rindt", new DateTime(1942, 4, 18));
            var p4 = new Person("Francois", "Cevert", new DateTime(1944, 2, 25));
            var coll = new PersonCollection(p1, p2, p3, p4);

            Console.WriteLine(coll[2]);

            foreach (var r in coll[new DateTime(1960, 3, 21)])
            {
                Console.WriteLine(r);
            }
            Console.ReadLine();
        }
    }
}
