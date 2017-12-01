using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Models
{
    public class MenuCardsContext : DbContext
    {
        public MenuCardsContext(DbContextOptions<MenuCardsContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>().Property(p => p.Text)
              .HasMaxLength(50).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
