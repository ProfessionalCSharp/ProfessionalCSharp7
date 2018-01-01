using BooksLib.Models;
using Framework;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public class BooksService : BindableBase, IItemsService<Book>
    {     
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private readonly IBooksRepository _booksRepository;

        public event EventHandler<Book> SelectedItemChanged;

        public BooksService(IBooksRepository repository)
        {
            _booksRepository = repository;
        }

        public ObservableCollection<Book> Items => _books;

        private Book _selectedItem;
        public Book SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Set(ref _selectedItem, value))
                {
                    SelectedItemChanged?.Invoke(this, _selectedItem);
                }
            }
        }

        public async Task<Book> AddOrUpdateAsync(Book book)
        {
            Book updated = null;
            if (book.BookId == 0)
            {
                updated = await _booksRepository.AddAsync(book);
            }
            else
            {
                updated = await _booksRepository.UpdateAsync(book);
            }
            return updated;
        }

        public Task DeleteAsync(Book book) =>
            _booksRepository.DeleteAsync(book.BookId);

        public async Task RefreshAsync()
        {
            IEnumerable<Book> books = await _booksRepository.GetItemsAsync();
            _books.Clear();
            foreach (var book in books)
            {
                _books.Add(book);
            }
            SelectedItem = Items.FirstOrDefault();
        }
    }
}
