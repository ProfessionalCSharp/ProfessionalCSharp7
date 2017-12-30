using BooksLib.Models;
using BooksLib.Services;
using Framework;
using Framework.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    public class ManageBooksViewModel : EditableMasterDetailViewModel<BookViewModel, Book>
    {
        private readonly IBooksService _booksService;
        private readonly ILogger<ManageBooksViewModel> _logger;
        private readonly IMessageService _showMessage;

        public ManageBooksViewModel(IBooksService booksService, ILogger<ManageBooksViewModel> logger, IMessageService showMessage)
        {
            _booksService = booksService;
            _logger = logger;
            _showMessage = showMessage;

            ReadBooksCommand = new RelayCommand(OnReadBooks, CanGetBooks);
            AddBookCommand = new RelayCommand(OnAddBook);
        }
        public RelayCommand ReadBooksCommand { get; }
        public RelayCommand AddBookCommand { get; }

        public async void OnReadBooks()
        {
            using (StartInProgress())
            {
                await RefreshBooksAsync();

                _canGetBooks = false;
                ReadBooksCommand.OnCanExecuteChanged();
            }
        }

        public async Task RefreshBooksAsync()
        {
            Items.Clear();
            var books = await _booksService.GetBooksAsync();
            
            foreach (var book in books)
            {
                Items.Add(new BookViewModel(book, this));
            }

            SelectedItem = Items.First();
        }

        private bool _canGetBooks = true;

        public bool CanGetBooks() => _canGetBooks;

        private void OnAddBook()
        {
            var bookVM = new BookViewModel(new Book(), this);
            Items.Add(bookVM);
            SelectedItem = bookVM;
            IsEditMode = true;
        }      

        protected override Book CreateCopyOfItem(Book book) =>
            new Book
            {
                BookId = book.BookId,
                Title = book.Title,
                Publisher = book.Publisher
            };

        protected async override void OnSave()
        {
            try
            {
                using (StartInProgress())
                {
                    await _booksService.AddOrUpdateBookAsync(EditItem);
                    await Task.Delay(5000); // simulate think time
                    var selectedId = SelectedItem.Item.BookId;
                    await RefreshBooksAsync();
                    SelectedItem = Items.Single(b => b.Item.BookId == selectedId);
                }
            }
            catch (Exception ex)
            {
                await _showMessage.ShowMessageAsync(ex.Message);
            } 
        }

        public async Task DeleteBookAsync(BookViewModel bookViewModel)
        {
            using (StartInProgress())
            {
                await _booksService.DeleteBookAsync(bookViewModel.Item);
                await RefreshBooksAsync();
                SelectedItem = Items.First();
            }
        }
    }
}
