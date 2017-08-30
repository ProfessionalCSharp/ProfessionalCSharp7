using System;

namespace RelationUsingAnnotations
{
    class Program
    {
        static void Main()
        {
            CreateDatabase();
            DeleteDatabase();
        }

        private static void DeleteDatabase()
        {
            using (var context = new BooksContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        private static void CreateDatabase()
        {
            using (var context = new BooksContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
