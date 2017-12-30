using BooksLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public interface IBooksService
    {
        // Task RefreshBooksAsync();

        // IEnumerable<Book> Books { get; }

        Task<IEnumerable<Book>> GetBooksAsync();

       // Book GetBook(int bookId);

        Task<Book> AddOrUpdateBookAsync(Book book);
    }
}
