using Microsoft.EntityFrameworkCore;

namespace TPHWithFluentAPI
{
    public static class ColumnNames
    {
        public const string Type = nameof(Type);
    }
    public static class ColumnValues
    {
        public const string Cash = nameof(Cash);
        public const string Creditcard = nameof(Creditcard);
    }

    public class BankContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;" +
            "Database=LocalBank;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasColumnType("Money");
            modelBuilder.Entity<Payment>().Property<string>(ColumnNames.Type); // shadow state for the discriminator
            modelBuilder.Entity<Payment>().HasDiscriminator<string>(ColumnNames.Type)
                .HasValue<CashPayment>(ColumnValues.Cash)
                .HasValue<CreditcardPayment>(ColumnValues.Creditcard);
        }

        public DbSet<Payment> Payments { get; set; }
    }
}
