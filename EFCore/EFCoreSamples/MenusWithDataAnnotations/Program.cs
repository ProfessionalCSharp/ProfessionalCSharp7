using System;

namespace MenusWithDataAnnotations
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();
        }

        public static void CreateDatabase()
        {
            using (var context = new MenusContext())
            {
                bool created = context.Database.EnsureCreated();
            }
        }
    }
}
