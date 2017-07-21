using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DynamicSample
{
    class Program
    {
        static void Main()
        {
            dynamic dyn;
            dyn = 100;
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(dyn);

            dyn = "This is a string";
            Console.WriteLine(dyn.GetType());
            Console.WriteLine(dyn);

            dyn = new Person() { FirstName = "Bugs", LastName = "Bunny" };
            Console.WriteLine(dyn.GetType());
            Console.WriteLine($"{dyn.FirstName} {dyn.LastName}");

            dyn = new WroxDynamicObject();
            dyn.FirstName = "Bugs";
            dyn.LastName = "Bunny";
            Console.WriteLine(dyn.GetType());
            Console.WriteLine($"{dyn.FirstName} {dyn.LastName}");

            dyn.MiddleName = "Rabbit";
            Console.WriteLine(dyn.MiddleName);
            Console.WriteLine(dyn.GetType());
            Console.WriteLine($"{dyn.FirstName} {dyn.MiddleName} {dyn.LastName}");
            List<Person> friends = new List<Person>();
            friends.Add(new Person() { FirstName = "Daffy", LastName = "Duck" });
            friends.Add(new Person() { FirstName = "Porky", LastName = "Pig" });
            friends.Add(new Person() { FirstName = "Tweety", LastName = "Bird" });
            dyn.Friends = friends;

            foreach (Person friend in dyn.Friends)
            {
                Console.WriteLine($"{friend.FirstName} {friend.LastName}");
            }

            Func<DateTime, string> GetTomorrow = today => today.AddDays(1).ToString("d");
            dyn.GetTomorrowDate = GetTomorrow;
            Console.WriteLine("Tomorrow is {0}", dyn.GetTomorrowDate(DateTime.Now));

            DoExpando();
            Console.Read();
        }

        static void DoExpando()
        {
            dynamic expObj = new ExpandoObject();
            expObj.FirstName = "Daffy";
            expObj.LastName = "Duck";
            Console.WriteLine(expObj.FirstName + " " + expObj.LastName);
            Func<DateTime, string> GetTomorrow = today => today.AddDays(1).ToString("d");
            expObj.GetTomorrowDate = GetTomorrow;
            Console.WriteLine($"Tomorrow is {expObj.GetTomorrowDate(DateTime.Now)}");

            expObj.Friends = new List<Person>();
            expObj.Friends.Add(new Person() { FirstName = "Bob", LastName = "Jones" });
            expObj.Friends.Add(new Person() { FirstName = "Robert", LastName = "Jones" });
            expObj.Friends.Add(new Person() { FirstName = "Bobby", LastName = "Jones" });

            foreach (Person friend in expObj.Friends)
            {
                Console.WriteLine($"{friend.FirstName} {friend.LastName}");
            }
        }
    }
}
