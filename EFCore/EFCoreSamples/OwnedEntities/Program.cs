using System;

namespace OwnedEntities
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();
            AddData();
            QueryPerson();
            DeleteDatabase();
        }

        private static void QueryPerson()
        {
            using (var context = new OwnedEntitiesContext())
            {
                foreach (var p in context.People)
                {
                    Console.WriteLine(p.Name);
                    Console.WriteLine($"Company address: {p.CompanyAddress.LineOne} {p.CompanyAddress.Location.City}");
                    Console.WriteLine($"Private address: {p.PrivateAddress.LineOne} {p.PrivateAddress.Location.City}");
                }
            }
        }

        private static void AddData()
        {
            using (var context = new OwnedEntitiesContext())
            {
                var p1 = new Person
                {
                     Name = "Tom Turbo",
                     CompanyAddress = new Address
                     {
                         LineOne = "Riesenradplatz",
                         Location = new Location
                         {
                             City = "Vienna",
                             Country = "Austria"
                         }
                     },
                     PrivateAddress = new Address
                     {
                         LineOne = "Tiergarten Schönbrunn",
                         LineTwo = "Seckendorff-Gudent-Weg",
                         Location = new Location
                         {
                             City = "Vienna",
                             Country = "Austria"
                         }
                     }
                };
                context.People.Add(p1);
                int records = context.SaveChanges();
            }
        }

        private static void CreateDatabase()
        {
            using (var context = new OwnedEntitiesContext())
            {
                bool created = context.Database.EnsureCreated();
                string creationInfo = created ? "created" : "exists";
                Console.WriteLine($"database {creationInfo}");
            }
        }

        private static void DeleteDatabase()
        {
            Console.Write("Delete the database? ");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                using (var context = new OwnedEntitiesContext())
                {
                    bool deleted = context.Database.EnsureDeleted();
                    string deletionInfo = deleted ? "deleted" : "not deleted";
                    Console.WriteLine($"database {deletionInfo}");
                }
            }
        }
    }
}
