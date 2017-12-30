using BooksLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetBooksAsync();

        Task<Book> AddOrUpdateBookAsync(Book book);

        Task DeleteBookAsync(Book book);
    }
}
