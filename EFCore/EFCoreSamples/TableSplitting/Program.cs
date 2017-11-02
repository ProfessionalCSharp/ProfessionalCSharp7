using System;

namespace TableSplitting
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();
            DeleteDatabase();
        }

        private static void CreateDatabase()
        {
            using (var context = new MenusContext())
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
                using (var context = new MenusContext())
                {
                    bool deleted = context.Database.EnsureDeleted();
                    string deletionInfo = deleted ? "deleted" : "not deleted";
                    Console.WriteLine($"database {deletionInfo}");
                }
            }
        }
    }
}
