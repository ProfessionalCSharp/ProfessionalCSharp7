using BooksLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public class BooksSampleRepository : IBooksRepository
    {
        private List<Book> _books;
        public BooksSampleRepository()
        {
            InitSampleBooks();
        }

        private void InitSampleBooks()
        {
            _books = new List<Book>()
            {
                new Book { BookId = 1, Title = "Professional C# 7 and .NET Core 2", Publisher = "Wrox Press" },
                new Book { BookId = 2, Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press" },
                new Book { BookId = 3, Title = "Professional C# 5.0 and .NET 4.5.1", Publisher = "Wrox Press" },
                new Book { BookId = 4, Title = "Enterprise Services with the .NET Framework", Publisher = "AWL" }
            };
        }

        public Task<bool> DeleteAsync(int id)
        {
            Book bookToDelete = _books.Find(b => b.BookId == id);
            if (bookToDelete != null)
            {
                return Task.FromResult(_books.Remove(bookToDelete));
            }
            return Task.FromResult(false);
        }

        public Task<Book> GetItemAsync(int id) =>
            Task.FromResult(_books.Find(b => b.BookId == id));

        public Task<IEnumerable<Book>> GetItemsAsync() => 
            Task.FromResult<IEnumerable<Book>>(_books);

        public Task<Book> UpdateAsync(Book item)
        {
            Book bookToUpdate = _books.Find(b => b.BookId == item.BookId);
            int ix = _books.IndexOf(bookToUpdate);
            _books[ix] = item;
            return Task.FromResult(_books[ix]);
        }

        public Task<Book> AddAsync(Book item)
        {
            item.BookId = _books.Select(b => b.BookId).Max() + 1;
            _books.Add(item);
            return Task.FromResult(item);
        }
    }
}
