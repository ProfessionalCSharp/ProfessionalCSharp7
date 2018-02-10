using DataLib;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EnumerableSample
{
    internal class LinqSamples
    {
        internal static void Register(CommandLineApplication app)
        {
            MethodInfo[] methods = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name == nameof(LinqSamples))
                .Single()
                .GetMethods()
                .Where(m => m.IsPublic && m.IsStatic)
                .ToArray();

            foreach (var method in methods)
            {
                app.Command(method.Name.ToLower(), cmd =>
                {
                    cmd.Description = method.Name;
                    cmd.OnExecute(() => { method.Invoke(null, null); return 0; });
                });
            }
        }

        public static void GenerateRange()
        {
            var values = Enumerable.Range(1, 20);
            foreach (var item in values)
            {
                Console.Write($"{item} ", item);
            }
            Console.WriteLine();
        }

        public static void Except()
        {
            var racers = Formula1.GetChampionships().SelectMany(cs => new List<RacerInfo>()
               {
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 1,
                   FirstName = cs.First.FirstName(),
                   LastName = cs.First.LastName()
                 },
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 2,
                   FirstName = cs.Second.FirstName(),
                   LastName = cs.Second.LastName()
                 },
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 3,
                   FirstName = cs.Third.FirstName(),
                   LastName = cs.Third.LastName()
                 }
               });

            var nonChampions = racers.Select(r =>
              new
              {
                  r.FirstName,
                  r.LastName
              }).Except(Formula1.GetChampions().Select(r =>
                new
                {
                    r.FirstName,
                    r.LastName
                }));

            foreach (var r in nonChampions)
            {
                Console.WriteLine($"{r.FirstName} {r.LastName}");
            }
        } 

        public static void ConvertWithCast()
        {
            var list = new System.Collections.ArrayList(Formula1.GetChampions() as System.Collections.ICollection);

            var query = from r in list.Cast<Racer>()
                        where r.Country == "USA"
                        orderby r.Wins descending
                        select r;
            foreach (var racer in query)
            {
                Console.WriteLine($"{racer:A}");
            }
        }

        public static void ZipOperation()
        {
            var racerNames = from r in Formula1.GetChampions()
                             where r.Country == "Italy"
                             orderby r.Wins descending
                             select new
                             {
                                 Name = r.FirstName + " " + r.LastName
                             };

            var racerNamesAndStarts = from r in Formula1.GetChampions()
                                      where r.Country == "Italy"
                                      orderby r.Wins descending
                                      select new
                                      {
                                          r.LastName,
                                          r.Starts
                                      };

            var racers = racerNames.Zip(racerNamesAndStarts, (first, second) => first.Name + ", starts: " + second.Starts);
            foreach (var r in racers)
            {
                Console.WriteLine(r);
            }
        }

        public static void ToLookup()
        {
            var racers = (from r in Formula1.GetChampions()
                          from c in r.Cars
                          select new
                          {
                              Car = c,
                              Racer = r
                          }).ToLookup(cr => cr.Car, cr => cr.Racer);

            if (racers.Contains("Williams"))
            {
                foreach (var williamsRacer in racers["Williams"])
                {
                    Console.WriteLine(williamsRacer);
                }
            }
        }

        public static void AggregateSum()
        {
            var countries = (from c in
                                 from r in Formula1.GetChampions()
                                 group r by r.Country into c
                                 select new
                                 {
                                     Country = c.Key,
                                     Wins = (from r1 in c
                                             select r1.Wins).Sum()
                                 }
                             orderby c.Wins descending, c.Country
                             select c).Take(5);

            foreach (var country in countries)
            {
                Console.WriteLine($"{country.Country} {country.Wins}");
            }
        }

        public static void AggregateCount()
        {
            var query = from r in Formula1.GetChampions()
                        let numberYears = r.Years.Count()
                        where numberYears >= 3
                        orderby numberYears descending, r.LastName
                        select new
                        {
                            Name = r.FirstName + " " + r.LastName,
                            TimesChampion = numberYears
                        };

            foreach (var r in query)
            {
                Console.WriteLine($"{r.Name} {r.TimesChampion}");
            }
        }

        public static void Partitioning()
        {
            int pageSize = 5;

            int numberPages = (int)Math.Ceiling(Formula1.GetChampions().Count() /
                  (double)pageSize);

            for (int page = 0; page < numberPages; page++)
            {
                Console.WriteLine($"Page {page}");

                var racers =
                   (from r in Formula1.GetChampions()
                    orderby r.LastName, r.FirstName
                    select r.FirstName + " " + r.LastName).
                   Skip(page * pageSize).Take(pageSize);

                foreach (var name in racers)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }

        public static void SetOperations()
        {
            IEnumerable<Racer> racersByCar(string car) =>
                from r in Formula1.GetChampions()
                from c in r.Cars
                where c == car
                orderby r.LastName
                select r;

            Console.WriteLine("World champion with Ferrari and McLaren");
            foreach (var racer in racersByCar("Ferrari").Intersect(racersByCar("McLaren")))
            {
                Console.WriteLine(racer);
            }
        }

        public static void ToList()
        {
            List<Racer> racers = (from r in Formula1.GetChampions()
                                  where r.Starts > 200
                                  orderby r.Starts descending
                                  select r).ToList();

            foreach (var racer in racers)
            {
                Console.WriteLine($"{racer} {racer:S}");
            }
        }
    }
}
