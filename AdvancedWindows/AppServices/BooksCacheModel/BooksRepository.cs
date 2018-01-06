using System.Collections.Generic;

namespace BooksCacheModel
{
    public class BooksRepository
    {
        private readonly List<Book> _books = new List<Book>()
        {
            new Book {Title = "Professional C# 7 and .NET Core 2", Publisher = "Wrox Press" }
        };
        public IEnumerable<Book> Books => _books;

        private BooksRepository()
        {
        }

        public static BooksRepository Instance = new BooksRepository();

        public void AddBook(Book book) => _books.Add(book);
    }
}
