using BooksLib.Models;
using Framework.Services;
using Framework.ViewModels;
using System;

namespace BooksLib.ViewModels
{
    // this view model is used to display details of a book and allows editing
    public class BookDetailViewModel : EditableItemViewModel<Book>
    {
        public BookDetailViewModel(IItemsService<Book> itemsService)
            : base(itemsService)
        {

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

        public override void OnSave()
        {

        }
    }
}
