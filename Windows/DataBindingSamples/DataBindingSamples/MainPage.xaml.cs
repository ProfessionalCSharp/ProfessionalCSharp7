using DataBindingSamples.Models;
using DataBindingSamples.Services;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace DataBindingSamples
{
    public sealed partial class MainPage : Page
    {
        private BooksService _booksService = new BooksService();
        public MainPage() => InitializeComponent();

        public void OnRefreshBooks() => _booksService.RefreshBooks();

        public void OnAddBook() =>
            _booksService.AddBook(new Book(GetNextBookId(), $"Professional C# {GetNextBookId() + 3}", "Wrox Press"));

        private int GetNextBookId() => Books.Select(b => b.BookId).Max() + 1;

        public IEnumerable<Book> Books => _booksService.Books;
    }
}
