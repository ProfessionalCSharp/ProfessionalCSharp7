using BooksLib.Models;
using BooksLib.Services;
using Framework.Services;
using Framework.ViewModels;
using System;

namespace BooksLib.ViewModels
{
    public class NavigationInfoEvent : EventArgs
    {
        public bool UseNavigation { get; set; }
    }

    public class BooksViewModel : MasterDetailViewModel<BookItemViewModel, Book>
    {
        private readonly IItemsService<Book> _booksService;
        private readonly INavigationService _navigationService;

        public BooksViewModel(IItemsService<Book> booksService, INavigationService navigationService)
            : base(booksService)
        {
            _booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            EventAggregator<NavigationInfoEvent>.Instance.Event += (sender, e) =>
            {
                UseNavigation = e.UseNavigation;
            };

            PropertyChanged += async (sender, e) =>
            {
                if (UseNavigation && e.PropertyName == nameof(SelectedItem) && _navigationService.CurrentPage == PageNames.BooksPage)
                {
                    await _navigationService.NavigateToAsync(PageNames.BookDetailPage);
                }
            };
        }

        public bool UseNavigation { get; set; }

        public override void OnAdd()
        {
            var newBook = new Book();
            Items.Add(newBook);
            SelectedItem = newBook;
        }

        protected override BookItemViewModel ToViewModel(Book item) => new BookItemViewModel(item, _booksService);
    }
}
