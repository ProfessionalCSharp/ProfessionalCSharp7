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
        private readonly IEditModeService _editModeService;

        public BooksViewModel(IItemsService<Book> booksService, ILogger<BooksViewModel> logger, IMessageService messageService, ISelectedItemService<Book> selectedItemService, IEditModeService editModeService)
            : base(booksService, editModeService)
        {
            _booksService = booksService;
            _logger = logger;
            _messageService = messageService;
            _editModeService = editModeService;
        }

        public override void OnAdd()
        {
            var newBook = new Book();
            Items.Add(newBook);
            SelectedItem = newBook;
            base.OnAdd();
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
