using System;

namespace ExpressionBodiedMembers
{
    class Program
    {
        static void Main(string[] args)
        {

            Person p = new Person("Katharina Nagel");
            Console.WriteLine($"{p.FirstName} {p.LastName}");

        }
    }
}
