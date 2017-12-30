using BooksLib.Models;
using BooksLib.Services;
using Framework;
using Framework.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    public class ManageBooksViewModel : EditableMasterDetailViewModel<BookViewModel, Book>
    {
        private readonly IBooksService _booksService;
        private readonly ILogger<ManageBooksViewModel> _logger;

        public ManageBooksViewModel(IBooksService booksService, ILogger<ManageBooksViewModel> logger)
        {
            _booksService = booksService;
            _logger = logger;

            GetBooksCommand = new RelayCommand(OnGetBooks, CanGetBooks);
            AddBookCommand = new RelayCommand(OnAddBook);
        }
        public RelayCommand GetBooksCommand { get; }
        public RelayCommand AddBookCommand { get; }

        public async void OnGetBooks()
        {
            // await _booksService.LoadBooksAsync();
            await RefreshBooksAsync();

            _canGetBooks = false;
           GetBooksCommand.OnCanExecuteChanged();
        }

        public async Task RefreshBooksAsync()
        {
            Items.Clear();
            var books = await _booksService.GetBooksAsync();
            
            foreach (var book in books)
            {
                Items.Add(new BookViewModel(book));
            }

            SelectedItem = Items.First();
        }

        private bool _canGetBooks = true;

        public bool CanGetBooks() => _canGetBooks;

        private void OnAddBook()
        {
          //  EventAggregator<BookInfoEvent>.Instance.Publish(this, new BookInfoEvent { BookId = 0 });
        }

       

        protected override Book GetSelectedItem()
        {
            return base.GetSelectedItem();
        }

        protected override Book CreateCopyOfItem(Book book) =>
            new Book
            {
                BookId = book.BookId,
                Title = book.Title,
                Publisher = book.Publisher
            };

        public override void BeginEdit()
        {
            base.BeginEdit();
        }


        public override void CancelEdit()
        {
            base.CancelEdit();
        }

        public override void EndEdit()
        {
            base.EndEdit();
        }

        protected async override void OnSave()
        {
            await _booksService.AddOrUpdateBookAsync(EditItem);
            var selectedId = SelectedItem.Item.BookId;
            await RefreshBooksAsync();
            SelectedItem = Items.Single(b => b.Item.BookId == selectedId);
        }
    }
}
