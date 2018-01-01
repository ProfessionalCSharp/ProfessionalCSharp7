using BooksLib.Models;
using BooksLib.Services;
using Framework.Services;
using Framework.ViewModels;
using Microsoft.Extensions.Logging;

namespace BooksLib.ViewModels
{
    public class BooksViewModel : MasterDetailViewModel<BookItemViewModel, Book>
    {
        private readonly IItemsService<Book> _booksService;
        private readonly ILogger<BooksViewModel> _logger;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;

        public BooksViewModel(IItemsService<Book> booksService, ILogger<BooksViewModel> logger, IMessageService messageService, INavigationService navigationService)
            : base(booksService)
        {
            _booksService = booksService;
            _logger = logger;
            _messageService = messageService;
            _navigationService = navigationService;

            PropertyChanged += async (sender, e) =>
            {
                if (e.PropertyName == nameof(SelectedItem) && _navigationService.CurrentPage == PageNames.BooksPage)
                {
                    await _navigationService.NavigateToAsync(PageNames.BookDetailPage);
                }
            };
        }

        public override void OnAdd()
        {
            var newBook = new Book();
            Items.Add(newBook);
            SelectedItem = newBook;
        }

        protected override BookItemViewModel ToViewModel(Book item) => new BookItemViewModel(item, _booksService);
    }
}
