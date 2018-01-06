using BooksLib.Models;

namespace BooksLib.Services
{
    public interface  IBooksRepository : IQueryRepository<Book, int>, IUpdateRepository<Book, int>
    {
    }
}
