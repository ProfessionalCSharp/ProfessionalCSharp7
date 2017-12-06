using Microsoft.EntityFrameworkCore;

namespace OwnedEntities
{
    public class OwnedEntitiesContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;" +
            "Database=OwnedEntities;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .OwnsOne(p => p.CompanyAddress)
                .OwnsOne(a => a.Location, builder =>
                {
                    builder.Property(p => p.City).HasColumnName("BusinessCity");
                    builder.Property(p => p.Country).HasColumnName("BusinessCountry");
                });
            modelBuilder.Entity<Person>().OwnsOne(p => p.PrivateAddress).ToTable("Addr").OwnsOne(a => a.Location);
        }

        public DbSet<Person> People { get; set; }
    }
}
