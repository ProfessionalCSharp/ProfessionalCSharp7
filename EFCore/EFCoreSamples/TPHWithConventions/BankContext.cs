using Microsoft.EntityFrameworkCore;

namespace TPHWithConventions
{
    public class BankContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;" +
            "Database=LocalBank;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }


        public DbSet<Payment> Payments { get; set; }
        public DbSet<CreditcardPayment> CreditcardPayments { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
    }
}
