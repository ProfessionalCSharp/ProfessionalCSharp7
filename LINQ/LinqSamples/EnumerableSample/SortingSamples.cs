using DataLib;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Linq;

namespace EnumerableSample
{
    class SortingSamples
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("sort", cmd =>
            {
                var methodOption = new CommandOption("-m", CommandOptionType.NoValue);
                var descendingOption = new CommandOption("-d", CommandOptionType.NoValue);
                var descendingMethodOption = new CommandOption("-dm", CommandOptionType.NoValue);
                cmd.Options.AddRange(new[] { methodOption, descendingOption, descendingMethodOption });
                cmd.Description = "join -[m|v|a|n|nm]";
                cmd.OnExecute(() =>
                {
                    if (methodOption.HasValue())
                    {
                        SortMultipleWithMethods();
                    }
                    else if (descendingOption.HasValue())
                    {
                        SortDescending();
                    }
                    else if (descendingMethodOption.HasValue())
                    {
                        SortDescendingWithMethods();
                    }
                    else
                    {
                        SortMultiple();
                    }
                    return 0;
                });
            });
        }

        public static void SortMultiple()
        {
            Console.WriteLine("Show the first 10 champions ordered by country, lastname, firstname");
            Console.WriteLine();

            var racers = (from r in Formula1.GetChampions()
                          orderby r.Country, r.LastName, r.FirstName
                          select r).Take(10);

            foreach (var racer in racers)
            {
                Console.WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
            }
        }

        public static void SortMultipleWithMethods()
        {
            var racers = Formula1.GetChampions()
                            .OrderBy(r => r.Country)
                            .ThenBy(r => r.LastName)
                            .ThenBy(r => r.FirstName)
                            .Take(10);

            foreach (var racer in racers)
            {
                Console.WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
            }
        }

        public static void SortDescending()
        {
            Console.WriteLine("Show all champions from Brazil ordered by wins descending");
            Console.WriteLine();

            var racers = from r in Formula1.GetChampions()
                         where r.Country == "Brazil"
                         orderby r.Wins descending
                         select r;

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        public static void SortDescendingWithMethods()
        {
            Console.WriteLine("Show all champions from Brazil ordered by wins descending");
            Console.WriteLine();
            var racers = Formula1.GetChampions()
                .Where(r => r.Country == "Brazil")
                .OrderByDescending(r => r.Wins);

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }
    }
}
