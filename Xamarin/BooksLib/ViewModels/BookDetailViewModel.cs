using BooksLib.Models;
using BooksLib.Services;
using Framework.Services;
using Framework.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    // this view model is used to display details of a book and allows editing
    public class BookDetailViewModel : EditableItemViewModel<Book>
    {
        private readonly IItemsService<Book> _itemsService;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly ILogger _logger;
        public BookDetailViewModel(IItemsService<Book> itemsService, INavigationService navigationService, IMessageService messageService, ILogger<BookDetailViewModel> logger)
            : base(itemsService)
        {
            _itemsService = itemsService ?? throw new ArgumentNullException(nameof(itemsService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _logger = logger;

            itemsService.SelectedItemChanged += (sender, book) =>
            {
                Item = book;
            };
        }

        public override Book CreateCopy(Book item) =>
            new Book
            {
                BookId = item?.BookId ?? -1,
                Title = item?.Title ?? "enter a title",
                Publisher = item?.Publisher ?? "enter a publisher"
            };

        protected override void OnAdd()
        {

        }

        public override async Task OnSaveAsync()
        {
            try
            {
                await _itemsService.AddOrUpdateAsync(EditItem);
            }
            catch (Exception ex)
            {
                _logger.LogError("error {0} in {1}", ex.Message, nameof(OnSaveAsync));
                await _messageService.ShowMessageAsync("Error saving the data");
            }
        }

        public override async Task OnEndEditAsync()
        {
            if (_navigationService.UseNavigation)
            {
                await _navigationService.GoBackAsync();
            }
        }  
    }
}
