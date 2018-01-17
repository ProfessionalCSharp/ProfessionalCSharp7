using System.Collections.Generic;
using System.Threading.Tasks;
using BooksLib.Models;

namespace BooksLib.Services
{
    public interface IBooksService
    {
        Task<Book> AddOrUpdateBookAsync(Book book);
        Book GetBook(int bookId);
        Task LoadBooksAsync();

        IEnumerable<Book> Books { get; }
    }
}