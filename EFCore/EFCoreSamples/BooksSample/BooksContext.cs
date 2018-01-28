using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BooksSample.ColumnNames;

namespace BooksSample
{
    internal class ColumnNames
    {
        public const string LastUpdated = nameof(LastUpdated);
        public const string IsDeleted = nameof(IsDeleted);
        public const string BookId = nameof(BookId);
        public const string AuthorId = nameof(AuthorId);
    }

    public class BooksContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=BooksSample;trusted_connection=true";
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString)
                   .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasQueryFilter(b => !EF.Property<bool>(b, IsDeleted));

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

            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
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
