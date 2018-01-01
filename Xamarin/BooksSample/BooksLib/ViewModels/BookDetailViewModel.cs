using BooksLib.Models;
using Framework.Services;
using Framework.ViewModels;
using System;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    // this view model is used to display details of a book and allows editing
    public class BookDetailViewModel : EditableItemViewModel<Book>
    {
        private readonly IItemsService<Book> _itemsService;
        private readonly INavigationService _navigationService;
        public BookDetailViewModel(IItemsService<Book> itemsService, INavigationService navigationService)
            : base(itemsService)
        {
            _itemsService = itemsService;
            _navigationService = navigationService;
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

        public async override Task OnSaveAsync()
        {
            await _itemsService.AddOrUpdateAsync(EditItem);
        }

        public override Task OnEndEditAsync() =>
            _navigationService.GoBackAsync();
        
    }
}
