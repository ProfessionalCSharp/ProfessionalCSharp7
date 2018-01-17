using DataLib;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableSample
{
    class JoinSamples
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("join", cmd =>
            {
                var methodOption = new CommandOption("-m", CommandOptionType.NoValue);
                var groupJoinOption = new CommandOption("-gj", CommandOptionType.NoValue);
                var groupJoinMethodOption = new CommandOption("-gjm", CommandOptionType.NoValue);
                var leftOuterJoinOption = new CommandOption("-lo", CommandOptionType.NoValue);
                var leftOuterJoinMethodOption = new CommandOption("-nm", CommandOptionType.NoValue);
                cmd.Options.AddRange(new[] { methodOption, groupJoinOption, groupJoinMethodOption, leftOuterJoinOption, leftOuterJoinMethodOption });
                cmd.Description = "join -[m|v|a|n|nm]";
                cmd.OnExecute(() =>
                {
                    if (methodOption.HasValue())
                    {
                        InnerJoinWithMethods();
                    }
                    else if (groupJoinOption.HasValue())
                    {
                        GroupJoin();
                    }
                    else if (groupJoinMethodOption.HasValue())
                    {
                        GroupJoinWithMethods();
                    }
                    else if (leftOuterJoinOption.HasValue())
                    {
                        LeftOuterJoin();
                    }
                    else if (leftOuterJoinMethodOption.HasValue())
                    {
                        LeftOuterJoinWithMethods();
                    }
                    else
                    {
                        InnerJoin();
                    }
                    return 0;
                });
            });
        }

        public static void InnerJoin()
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
                            t.Name
                        };

            var racersAndTeams =
                  (from r in racers
                   join t in teams on r.Year equals t.Year
                   orderby t.Year
                   select new
                   {
                       r.Year,
                       Champion = r.Name,
                       Constructor = t.Name
                   }).Take(10);

            Console.WriteLine("Year  World Champion\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }

        public static void InnerJoinWithMethods()
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
                    t.Name
                });

            var racersAndTeams = racers.Join(
                teams,
                r => r.Year,
                t => t.Year,
                (r, t) =>
                    new
                    {
                        r.Year,
                        Champion = r.Name,
                        Constructor = t.Name
                    }).OrderBy(item => item.Year).Take(10);

            Console.WriteLine("Year  World Champion\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }

        public static void GroupJoin()
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

        public static void GroupJoinWithMethods()
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

        public static void LeftOuterJoinWithMethods()
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

        public static void LeftOuterJoin()
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
                            t.Name
                        };

            var racersAndTeams =
              (from r in racers
               join t in teams on r.Year equals t.Year into rt
               from t in rt.DefaultIfEmpty()
               orderby r.Year
               select new
               {
                   r.Year,
                   Champion = r.Name,
                   Constructor = t == null ? "no constructor championship" : t.Name
               }).Take(10);

            Console.WriteLine("Year  Champion\t\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }
    }
}
