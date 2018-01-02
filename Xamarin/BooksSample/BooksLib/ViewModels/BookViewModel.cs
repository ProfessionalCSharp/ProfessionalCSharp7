using BooksLib.Models;
using Framework;
using Framework.ViewModels;

namespace BooksLib.ViewModels
{
    public class BookViewModel : ItemViewModel<Book>
    {
        private readonly BooksViewModel _viewModel;

        public BookViewModel(Book book, BooksViewModel manageBooksViewModel)
        {
            _viewModel = manageBooksViewModel;
            Item = book;
            DeleteBookCommand = new RelayCommand(OnDeleteBook);
        }

        public RelayCommand DeleteBookCommand { get; set; }

        private async void OnDeleteBook()
        {
           // await _viewModel.DeleteBookAsync(this);
        }
    }
}
