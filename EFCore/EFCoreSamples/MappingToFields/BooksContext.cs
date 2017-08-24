using Microsoft.EntityFrameworkCore;

namespace MappingToFields
{
    public class BooksContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=Books;trusted_connection=true";
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Publisher).IsRequired(false).HasMaxLength(30);

            modelBuilder.Entity<Book>().Property(b => b.Title)
                .HasField("_bookId")
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Publisher)
                .HasField("_publisher")
                .IsRequired(false)
                .HasMaxLength(30);

            modelBuilder.Entity<Book>().Property<int>("BookId").HasField("_bookId").IsRequired();
            modelBuilder.Entity<Book>().HasKey("BookId");
        }
    }
}
