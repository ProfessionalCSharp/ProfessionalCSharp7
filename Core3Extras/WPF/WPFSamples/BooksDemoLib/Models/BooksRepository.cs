using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BooksDemo.Models
{
    public class BooksRepository
    {
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();

        public BooksRepository()
        {
            _books.Add(new Book("Professional C# 5.0 and .NET 4.5.1", "Wrox Press", "978-1-118-83303-2", "Christian Nagel", "Jay Glynn", "Morgan Skinner"));
            _books.Add(new Book("Professional C# 2012 and .NET 4.5", "Wrox Press", "978-1-118-31442-5 ", "Christian Nagel", "Jay Glynn", "Morgan Skinner"));
            _books.Add(new Book("Professional C# 4 with .NET 4", "Wrox Press", "978-0-470-50225-9", "Christian Nagel", "Bill Evjen", "Jay Glynn", "Karli Watson", "Morgan Skinner"));
            _books.Add(new Book("Beginning Visual C# 2010", "Wrox Press", "978-0-470-50226-6", "Karli Watson", "Christian Nagel", "Jacob Hammer Pedersen", "Jon D. Reid", "Morgan Skinner"));
            _books.Add(new Book("Windows 8 Secrets", "Wiley", "978-1-118-20413-9", "Paul Thurrott", "Rafael Rivera"));
            _books.Add(new Book("C# 5 All-in-One for Dummies", "For Dummies", "978-1-118-38536-4", "Bill Sempf", "Chuck Sphar", "Stephen R. Davis"));
        }

        public Book GetTheBook() => _books[0];

        public void AddBook(Book book) 
            => _books.Add(book);

        public IEnumerable<Book> GetBooks() => _books;
  }
}
