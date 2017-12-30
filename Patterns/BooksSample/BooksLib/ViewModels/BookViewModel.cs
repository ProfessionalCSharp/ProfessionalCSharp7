using BooksLib.Models;
using Framework.ViewModels;

namespace BooksLib.ViewModels
{
    public class BookViewModel : ItemViewModel<Book>
    {
        public BookViewModel(Book book)
            : base(book)
        {
        }
    }
}
