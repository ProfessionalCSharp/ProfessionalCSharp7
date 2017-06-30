using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Collections
{
    class Program
    {
        static void Main()
        {
            var idJimmie = new EmployeeId("C48");
            var jimmie = new Employee(idJimmie, "Jimmie Johnson", 150926.00m);

            var idJoey = new EmployeeId("F22");
            var joey = new Employee(idJoey, "Joey Logano", 45125.00m);

            var idKyle = new EmployeeId("T18");
            var kyle = new Employee(idKyle, "Kyle Bush", 78728.00m);

            var idCarl = new EmployeeId("T19");
            var carl = new Employee(idCarl, "Carl Edwards", 80473.00m);

            var idMatt = new EmployeeId("T20");
            var matt = new Employee(idMatt, "Matt Kenseth", 113970.00m);

            var employees = new Dictionary<EmployeeId, Employee>(31)
            {
                [idJimmie] = jimmie,
                [idJoey] = joey,
                [idKyle] = kyle,
                [idCarl] = carl,
                [idMatt] = matt
            };

            foreach (var employee in employees.Values)
            {
                Console.WriteLine(employee);
            }

            while (true)
            {
                Console.Write("Enter employee id (X to exit)> ");
                var userInput = Console.ReadLine();
                userInput = userInput.ToUpper();
                if (userInput == "X") break;

                EmployeeId id;
                try
                {
                    id = new EmployeeId(userInput);

                    if (!employees.TryGetValue(id, out Employee employee))
                    {
                        Console.WriteLine($"Employee with id {id} does not exist");
                    }
                    else
                    {
                        Console.WriteLine(employee);
                    }
                }
                catch (EmployeeIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
