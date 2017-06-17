using System;

namespace ClassesSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person("Katharina", "Nagel");
            Console.WriteLine($"{p.FirstName} {p.LastName}");
        }
    }
}
