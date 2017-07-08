using DataLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableSample
{
    class Program
    {
        private static readonly Command[] commands =
        {
            new Command("-f", nameof(Filtering), Filtering),
            new Command("-fm", nameof(FilteringWithMethods), FilteringWithMethods),
            new Command("-fi", nameof(FilteringWithIndex), FilteringWithIndex),
            new Command("-tf", nameof(TypeFiltering), TypeFiltering),
            new Command("-cf", nameof(CompoundFrom), CompoundFrom),
            new Command("-cfm", nameof(CompoundFromWithMethods), CompoundFromWithMethods),
            new Command("-sd", nameof(SortDescending), SortDescending),
            new Command("-sdm", nameof(SortDescendingWithMethods), SortDescendingWithMethods),
            new Command("-sm", nameof(SortMultiple), SortMultiple),
            new Command("-smm", nameof(SortMultipleWithMethods), SortMultipleWithMethods),
            new Command("-g", nameof(Grouping), Grouping),
            new Command("-gm", nameof(GroupingWithMethods), GroupingWithMethods),
            new Command("-gv", nameof(GroupingWithVariables), GroupingWithVariables),
            new Command("-ga", nameof(GroupingWithAnonymousTypes), GroupingWithAnonymousTypes),
            new Command("-gn", nameof(GroupingAndNestedObjects), GroupingAndNestedObjects),
            new Command("-gnm", nameof(GroupingAndNestedObjectsWithMethods), GroupingAndNestedObjectsWithMethods),
            new Command("-ij", nameof(InnerJoin), InnerJoin),
            new Command("-ijm", nameof(InnerJoinWithMethods), InnerJoinWithMethods),
            new Command("-loj", nameof(LeftOuterJoin), LeftOuterJoin),
            new Command("-lojm", nameof(LeftOuterJoinWithMethods), LeftOuterJoinWithMethods),
            new Command("-gj", nameof(GroupJoin), GroupJoin),
            new Command("-gjm", nameof(GroupJoinWithMethods), GroupJoinWithMethods),
            new Command("-set", nameof(SetOperations), SetOperations),
            new Command("-zip", nameof(ZipOperation), ZipOperation),
            new Command("-pages", nameof(Partitioning), Partitioning),
            new Command("-aggcount", nameof(AggregateCount), AggregateCount),
            new Command("-aggsum", nameof(AggregateSum), AggregateSum),
            new Command("-tolist", nameof(ToList), ToList),
            new Command("-tolookup", nameof(ToLookup), ToLookup),
            new Command("-cast", nameof(ConvertWithCast), ConvertWithCast),
            new Command("-generate", nameof(GenerateRange), GenerateRange),

        };

        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 1 || !commands.Select(c => c.Option).Contains(args[0]))
            {
                ShowUsage();
                return;
            }

            commands.Single(c => c.Option == args[0]).Action();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: EnumerableSample [options]");
            Console.WriteLine();
            foreach (var command in commands)
            {
                Console.WriteLine($"{command.Option} {command.Text}");
            }
        }

        static void GenerateRange()
        {
            var values = Enumerable.Range(1, 20);
            foreach (var item in values)
            {
                Console.Write($"{item} ", item);
            }
            Console.WriteLine();
        }

        private static void Except()
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
                  FirstName = r.FirstName,
                  LastName = r.LastName
              }).Except(Formula1.GetChampions().Select(r =>
                new
                {
                    FirstName = r.FirstName,
                    LastName = r.LastName
                }));

            foreach (var r in nonChampions)
            {
                Console.WriteLine($"{r.FirstName} {r.LastName}");
            }
        }

        private static void SortMultiple()
        {
            var racers = (from r in Formula1.GetChampions()
                          orderby r.Country, r.LastName, r.FirstName
                          select r).Take(10);

            foreach (var racer in racers)
            {
                Console.WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
            }
        }

        private static void SortMultipleWithMethods()
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

        private static void SortDescending()
        {
            var racers = from r in Formula1.GetChampions()
                         where r.Country == "Brazil"
                         orderby r.Wins descending
                         select r;

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        private static void SortDescendingWithMethods()
        {
            var racers = Formula1.GetChampions()
                .Where(r => r.Country == "Brazil")
                .OrderByDescending(r => r.Wins);

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        static void ConvertWithCast()
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

        static void ZipOperation()
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

        static void ToLookup()
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

        static void AggregateSum()
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

        static void AggregateCount()
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

        static void Partitioning()
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

        static void SetOperations()
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

        static void InnerJoin()
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams =
                  (from r in racers
                   join t in teams on r.Year equals t.Year
                   orderby t.Year
                   select new
                   {
                       Year = r.Year,
                       Champion = r.Name,
                       Constructor = t.Name
                   }).Take(10);

            Console.WriteLine("Year  World Champion\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }

        static void InnerJoinWithMethods()
        {
            var racers = Formula1.GetChampions()
                .SelectMany(r => r.Years, (r1, year) =>
                new
                {
                    Year = year,
                    Name = $"{r1.FirstName} {r1.LastName}"
                });

            var teams = Formula1.GetConstructorChampions()
                .SelectMany(t => t.Years, (t, year) =>
                new
                {
                    Year = year,
                    Name = t.Name
                });

            var racersAndTeams = racers.Join(
                teams,
                r => r.Year,
                t => t.Year,
                (r, t) =>
                    new
                    {
                        Year = r.Year,
                        Champion = r.Name,
                        Constructor = t.Name
                    }).OrderBy(item => item.Year).Take(10);

            Console.WriteLine("Year  World Champion\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }

        static void GroupJoin()
        {
            var racers = from cs in Formula1.GetChampionships()
                         from r in new List<(int Year, int Position, string FirstName, string LastName)>()
                         {
                             (cs.Year, Position: 1, FirstName: cs.First.FirstName(), LastName: cs.First.LastName()),
                             (cs.Year, Position: 2, FirstName: cs.Second.FirstName(), LastName: cs.Second.LastName()),
                             (cs.Year, Position: 3, FirstName: cs.Third.FirstName(), LastName: cs.Third.LastName())
                         }
                         select r;

            var q = (from r in Formula1.GetChampions()
                     join r2 in racers on
                     (
                         r.FirstName,
                         r.LastName
                     )
                     equals
                     (
                         r2.FirstName,
                         r2.LastName
                     )
                     into yearResults
                     select
                     (
                         r.FirstName,
                         r.LastName,
                         r.Wins,
                         r.Starts,
                         Results: yearResults
                     ));

            foreach (var r in q)
            {
                Console.WriteLine($"{r.FirstName} {r.LastName}");
                foreach (var results in r.Results)
                {
                    Console.WriteLine($"\t{results.Year} {results.Position}");
                }
            }
        }

        static void GroupJoinWithMethods()
        {
            var racers = Formula1.GetChampionships()
              .SelectMany(cs => new List<(int Year, int Position, string FirstName, string LastName)>
              {
                 (cs.Year, Position: 1, FirstName: cs.First.FirstName(), LastName: cs.First.LastName()),
                 (cs.Year, Position: 2, FirstName: cs.Second.FirstName(), LastName: cs.Second.LastName()),
                 (cs.Year, Position: 3, FirstName: cs.Third.FirstName(), LastName: cs.Third.LastName())
              });

            var q = Formula1.GetChampions()
                .GroupJoin(racers,
                    r1 => (r1.FirstName, r1.LastName),
                    r2 => (r2.FirstName, r2.LastName),
                    (r1, r2s) => (r1.FirstName, r1.LastName, r1.Wins, r1.Starts, Results: r2s));


            foreach (var r in q)
            {
                Console.WriteLine($"{r.FirstName} {r.LastName}");
                foreach (var results in r.Results)
                {
                    Console.WriteLine($"{results.Year} {results.Position}");
                }
            }
        }


        static void LeftOuterJoinWithMethods()
        {
            var racers = Formula1.GetChampions()
                .SelectMany(r => r.Years, (r1, year) =>
                new
                {
                    Year = year,
                    Name = $"{r1.FirstName} {r1.LastName}"
                });

            var teams = Formula1.GetConstructorChampions()
                .SelectMany(t => t.Years, (t, year) =>
                new
                {
                    Year = year,
                    Name = t.Name
                });

            var racersAndTeams =
                racers.GroupJoin(
                    teams,
                    r => r.Year,
                    t => t.Year,
                    (r, ts) => new
                    {
                        Year = r.Year,
                        Champion = r.Name,
                        Constructors = ts
                    })
                    .SelectMany(
                        item => item.Constructors.DefaultIfEmpty(),
                        (r, t) => new
                        {
                            Year = r.Year,
                            Champion = r.Champion,
                            Constructor = t?.Name ?? "no constructor championship"
                        });

            Console.WriteLine("Year  Champion\t\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }

        static void LeftOuterJoin()
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams =
              (from r in racers
               join t in teams on r.Year equals t.Year into rt
               from t in rt.DefaultIfEmpty()
               orderby r.Year
               select new
               {
                   Year = r.Year,
                   Champion = r.Name,
                   Constructor = t == null ? "no constructor championship" : t.Name
               }).Take(10);

            Console.WriteLine("Year  Champion\t\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }

        static void GroupingAndNestedObjects()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count,
                                Racers = from r1 in g
                                         orderby r1.LastName
                                         select r1.FirstName + " " + r1.LastName
                            };
            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
                foreach (var name in item.Racers)
                {
                    Console.Write($"{name}; ");
                }
                Console.WriteLine();
            }
        }

        static void GroupingAndNestedObjectsWithMethods()
        {
            var countries = Formula1.GetChampions()
                .GroupBy(r => r.Country)
                .Select(g => new
                {
                    Group = g,
                    Key = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.Key)
                .Where(g => g.Count >= 2)
                .Select(g => new
                {
                    Country = g.Key,
                    Count = g.Count,
                    Racers = g.Group.OrderBy(r => r.LastName).Select(r => r.FirstName + " " + r.LastName)
                });

            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
                foreach (var name in item.Racers)
                {
                    Console.Write($"{name}; ");
                }
                Console.WriteLine();
            }
        }

        static void Grouping()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            orderby g.Count() descending, g.Key
                            where g.Count() >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = g.Count()
                            };

            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        static void GroupingWithMethods()
        {
            var countries = Formula1.GetChampions()
              .GroupBy(r => r.Country)
              .OrderByDescending(g => g.Count())
              .ThenBy(g => g.Key)
              .Where(g => g.Count() >= 2)
              .Select(g => new
              {
                  Country = g.Key,
                  Count = g.Count()
              });


            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        static void GroupingWithVariables()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count
                            };

            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        static void GroupingWithAnonymousTypes()
        {
            var countries = Formula1.GetChampions()
              .GroupBy(r => r.Country)
              .Select(g => new { Group = g, Count = g.Count() })
              .OrderByDescending(g => g.Count)
              .ThenBy(g => g.Group.Key)
              .Where(g => g.Count >= 2)
              .Select(g => new
              {
                  Country = g.Group.Key,
                  Count = g.Count
              });

            foreach (var item in countries)
            {
                Console.WriteLine($"{item.Country,-10} {item.Count}");
            }
        }

        static void CompoundFrom()
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

        static void CompoundFromWithMethods()
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

        static void TypeFiltering()
        {
            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            foreach (var s in query)
            {
                Console.WriteLine(s);
            }
        }

        static void FilteringWithIndex()
        {
            var racers = Formula1.GetChampions().
                    Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        static void Filtering()
        {
            var racers = from r in Formula1.GetChampions()
                         where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria")
                         select r;

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        static void FilteringWithMethods()
        {
            var racers = Formula1.GetChampions()
                            .Where(r => r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria"));

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        static void ToList()
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
