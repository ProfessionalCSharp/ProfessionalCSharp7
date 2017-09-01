using System;

namespace MenusSample
{
    class Program
    {
        static void Main()
        {
            CreateDatabase();
            InitialFill();
            DeleteDatabase();
        }

        private static void InitialFill()
        {
            MenuCard[] cards = new MenuCard[]
            {
                new MenuCard { }
            };
            using (var context = new MenusContext())
            {

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
            using (var context = new MenusContext())
            {
                bool deleted = context.Database.EnsureDeleted();
            }
        }
    }
}
