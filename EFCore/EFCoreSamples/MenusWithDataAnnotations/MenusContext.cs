using Microsoft.EntityFrameworkCore;

namespace MenusWithDataAnnotations
{
    public class MenusContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;" +
            "Database=MenuCards2;Trusted_Connection=True";
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }

}
