using BooksLib.Models;
using Framework;
using Framework.ViewModels;

namespace BooksLib.ViewModels
{
    public class BookViewModel : ItemViewModel<Book>
    {
        private readonly ManageBooksViewModel _viewModel;

        public BookViewModel(Book book, ManageBooksViewModel manageBooksViewModel)
            : base(book)
        {
            _viewModel = manageBooksViewModel;
            DeleteBookCommand = new RelayCommand(OnDeleteBook);
        }

        public RelayCommand DeleteBookCommand { get; set; }

        private async void OnDeleteBook()
        {
            await _viewModel.DeleteBookAsync(this);
        }
    }
}
