using DataBindingSamples.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataBindingSamples.Services
{
    public class BooksService 
    {
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();

        public void RefreshBooks()
        {
            _books.Clear();
            var sampleBooksService = new SampleBooksService();
            var books = sampleBooksService.GetSampleBooks();
            foreach (var book in books)
            {
                _books.Add(book);
            }    
        }

        public Book GetBook(int bookId) =>
            _books.Where(b => b.BookId == bookId).SingleOrDefault();

        public void AddBook(Book book) => _books.Add(book);

        public IEnumerable<Book> Books => _books;
    }
}
