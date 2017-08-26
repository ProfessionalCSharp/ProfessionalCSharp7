using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static MappingToFields.ColumnNames;

namespace MappingToFields
{
    public class ColumnNames
    {
        public const string LastUpdated = nameof(LastUpdated);
        public const string IsDeleted = nameof(IsDeleted);
        public const string BookId = nameof(BookId);
    }

    public class BooksContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=Books2;trusted_connection=true";
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Publisher)
                .HasField("_publisher")
                .IsRequired(false)
                .HasMaxLength(30);

            modelBuilder.Entity<Book>().Property<int>(BookId)
                .HasField("_bookId")
                .IsRequired();
            modelBuilder.Entity<Book>()
                .HasKey(BookId);

            // shadow properties
            modelBuilder.Entity<Book>().Property<bool>(IsDeleted);
            modelBuilder.Entity<Book>().Property<DateTime>(LastUpdated);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<Book>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            {
                item.CurrentValues[LastUpdated] = DateTime.Now;

                if (item.State == EntityState.Deleted)
                {
                    item.State = EntityState.Modified;
                    item.CurrentValues[IsDeleted] = true;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges() => SaveChangesAsync().Result;
    }
}
