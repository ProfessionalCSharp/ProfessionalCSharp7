using Microsoft.EntityFrameworkCore;

namespace NullableSampleApp
{
#nullable enable
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = default!;
    }
#nullable restore
}
