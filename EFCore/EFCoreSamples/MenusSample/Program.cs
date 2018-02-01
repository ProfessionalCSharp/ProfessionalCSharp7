using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Diagnostics;
using System.Linq;

namespace MenusSample
{
    class Program
    {
        static void Main()
        {
            CreateDatabase();
            AddRecords();
            ObjectTracking();
            UpdateRecords();
            ChangeUntracked();
            AddHundredRecords();
            DeleteDatabase();
        }

        private static void AddHundredRecords()
        {
            Console.WriteLine(nameof(AddHundredRecords));
            using (var context = new MenusContext())
            {
                var card = context.MenuCards.FirstOrDefault();
                if (card != null)
                {
                    var menus = Enumerable.Range(1, 100).Select(x => new Menu
                    {
                        MenuCard = card,
                        Text = $"menu {x}",
                        Price = 9.9m
                    });
                    context.Menus.AddRange(menus);
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    int records = context.SaveChanges();
                    stopwatch.Stop();
                    Console.WriteLine($"{records} records added after {stopwatch.ElapsedMilliseconds} milliseconds");
                }
            }
            Console.WriteLine();
        }

        private static void AddRecords()
        {
            Console.WriteLine(nameof(AddRecords));
            try
            {
                using (var context = new MenusContext())
                {

                    var soupCard = new MenuCard();
                    Menu[] soups =
                    {
                        new Menu
                        {
                            Text = "Consommé Célestine (with shredded pancake)",
                            Price = 4.8m,
                            MenuCard =soupCard
                        },
                        new Menu
                        {
                            Text = "Baked Potato Soup",
                            Price = 4.8m,
                            MenuCard = soupCard
                        },
                        new Menu
                        {
                            Text = "Cheddar Broccoli Soup",
                            Price = 4.8m,
                            MenuCard = soupCard
                        },
                    };

                    soupCard.Title = "Soups";
                    soupCard.Menus.AddRange(soups);

                    context.MenuCards.Add(soupCard);

                    ShowState(context);

                    int records = context.SaveChanges();
                    Console.WriteLine($"{records} added");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
        }

        public static void ShowState(MenusContext context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                Console.WriteLine($"type: {entry.Entity.GetType().Name}, state: {entry.State}," +
                $" {entry.Entity}");
            }
        }

        private static void ObjectTracking()
        {
            Console.WriteLine(nameof(ObjectTracking));
            using (var context = new MenusContext())
            {
                var m1 = (from m in context.Menus
                          where m.Text.StartsWith("Con")
                          select m).FirstOrDefault();
                var m2 = (from m in context.Menus
                          where m.Text.Contains("(")
                          select m).FirstOrDefault();
                if (object.ReferenceEquals(m1, m2))
                {
                    Console.WriteLine("the same object");
                }
                else
                {
                    Console.WriteLine("not the same");
                }
                ShowState(context);
            }
            Console.WriteLine();
        }

        private static void UpdateRecords()
        {
            Console.WriteLine(nameof(UpdateRecords));
            using (var context = new MenusContext())
            {
                Menu menu = context.Menus
                  .Skip(1)
                  .FirstOrDefault();
                ShowState(context);
                menu.Price += 0.2m;
                ShowState(context);
                int records = context.SaveChanges();
                Console.WriteLine($"{records} updated");
                ShowState(context);
            }
            Console.WriteLine();
        }

        private static void ChangeUntracked()
        {
            Console.WriteLine(nameof(ChangeUntracked));
            Menu GetMenu()
            {
                using (var context = new MenusContext())
                {
                    Menu menu = context.Menus
                      .Skip(2)
                      .FirstOrDefault();
                    return menu;
                }
            }

            Menu m = GetMenu();
            m.Price += 0.7m;
            UpdateUntracked(m);
        }

        private static void UpdateUntracked(Menu m)
        {
            using (var context = new MenusContext())
            {
                ShowState(context);
                // EntityEntry<Menu> entry = context.Menus.Attach(m);
                // entry.State = EntityState.Modified;
                context.Menus.Update(m);
                ShowState(context);
                context.SaveChanges();
            }
        }





        public static void CreateDatabase()
        {
            using (var context = new MenusContext())
            {
                bool created = context.Database.EnsureCreated();
            }
        }

        public static void DeleteDatabase()
        {
            Console.Write("Delete the database? ");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                using (var context = new MenusContext())
                {
                    bool deleted = context.Database.EnsureDeleted();
                }
            }
        }
    }
}
