using BooksLib.Models;
using Framework.Services;
using Framework.ViewModels;
using System;

namespace BooksLib.ViewModels
{
    // this view model is used to display details of a book and allows editing
    public class BookDetailViewModel : EditableItemViewModel<Book>
    {
        public BookDetailViewModel(ISelectedItemService<Book> selectedItemService)
            : base(selectedItemService)
        {

        }

        public override Book CreateCopyOfItem(Book item)
        {
            throw new NotImplementedException();
        }

        protected override void OnAdd()
        {

        }

        public override void OnSave()
        {

        }
    }
}
