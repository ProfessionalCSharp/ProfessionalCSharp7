using BooksLib.Models;
using BooksLib.Services;
using Framework.Services;
using Framework.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    public class BooksViewModel : MasterDetailViewModel<BookItemViewModel, Book>
    {
        private readonly IItemsService<Book> _booksService;
        private readonly ILogger<BooksViewModel> _logger;
        private readonly IMessageService _messageService;

        public BooksViewModel(IItemsService<Book> booksService, ILogger<BooksViewModel> logger, IMessageService messageService)
            : base(booksService)
        {
            _booksService = booksService;
            _logger = logger;
            _messageService = messageService;
        }

        public override void OnAdd()
        {
            var newBook = new Book();
            Items.Add(newBook);
            SelectedItem = newBook;
        }

        //protected override Book CreateCopyOfItem(Book book) =>
        //    new Book
        //    {
        //        BookId = book.BookId,
        //        Title = book.Title,
        //        Publisher = book.Publisher
        //    };

        //protected async override void OnSave()
        //{
        //    try
        //    {
        //        using (StartInProgress())
        //        {
        //            await _booksService.AddOrUpdateBookAsync(EditItem);
        //            await Task.Delay(5000); // simulate think time
        //            var selectedId = SelectedItem.Item.BookId;
        //            await RefreshBooksAsync();
        //            SelectedItem = Items.Single(b => b.Item.BookId == selectedId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _messageService.ShowMessageAsync(ex.Message);
        //    } 
        //}

        //public async Task DeleteBookAsync(BookViewModel bookViewModel)
        //{
        //    using (StartInProgress())
        //    {
        //        await _booksService.DeleteBookAsync(bookViewModel.Item);
        //        await RefreshBooksAsync();
        //    }
        //}

        protected override BookItemViewModel ToViewModel(Book item) => new BookItemViewModel(item, _booksService);

    }
}
