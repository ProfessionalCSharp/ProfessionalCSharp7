using BooksLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public class BooksService : IBooksService
    {
     //   private readonly List<Book> _books = new List<Book>();
        private readonly IBooksRepository _booksRepository;
        public BooksService(IBooksRepository repository)
        {
            _booksRepository = repository;
        }

        public async Task<Book> AddOrUpdateBookAsync(Book book)
        {
            Book updated = null;
            if (book.BookId == 0)
            {
                updated = await _booksRepository.AddAsync(book);
            }
            else
            {
                updated = await _booksRepository.UpdateAsync(book);
            }
            return updated;
        }

        public Task<IEnumerable<Book>> GetBooksAsync() => 
            _booksRepository.GetItemsAsync();
    }
}
