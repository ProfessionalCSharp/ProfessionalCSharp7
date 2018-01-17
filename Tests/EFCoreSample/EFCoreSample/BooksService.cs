using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCoreSample
{
    public class BooksService
    {
        private readonly BooksContext _booksContext;

        public BooksService(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public IEnumerable<Book> GetTopBooksByPublisher(string publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));

            return _booksContext.Books.Where(b => b.Publisher == publisher).Take(10).ToList();
        }
    }
}
