using Microsoft.EntityFrameworkCore;

namespace EFCoreSample
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) { }
        public DbSet<Book> Books { get; set; }
    }
}
