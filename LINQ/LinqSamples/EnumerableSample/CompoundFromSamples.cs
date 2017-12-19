using DataLib;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumerableSample
{
    class CompoundFromSamples
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("compound", cmd =>
            {
                var methodOption = new CommandOption("-m", CommandOptionType.NoValue);
                cmd.Options.AddRange(new[] { methodOption });
                cmd.Description = "compound -[m]";
                cmd.OnExecute(() =>
                {
                    if (methodOption.HasValue())
                    {
                        CompoundFromWithMethods();
                    }
                    else
                    {
                        CompoundFrom();
                    }
                    return 0;
                });
            });
        }

        public static void CompoundFrom()
        {
            var ferrariDrivers = from r in Formula1.GetChampions()
                                 from c in r.Cars
                                 where c == "Ferrari"
                                 orderby r.LastName
                                 select $"{r.FirstName} {r.LastName}";

            foreach (var racer in ferrariDrivers)
            {
                Console.WriteLine(racer);
            }
        }

        public static void CompoundFromWithMethods()
        {
            var ferrariDrivers = Formula1.GetChampions()
                .SelectMany(r => r.Cars, (r1, cars) => new { Racer1 = r1, Cars1 = cars })
                .Where(item => item.Cars1.Contains("Ferrari"))
                .OrderBy(item => item.Racer1.LastName)
                .Select(item => $"{item.Racer1.FirstName} {item.Racer1.LastName}");

            foreach (var racer in ferrariDrivers)
            {
                Console.WriteLine(racer);
            }
        }
    }
}
