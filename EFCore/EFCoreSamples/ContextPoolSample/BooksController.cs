using System.Threading.Tasks;

namespace ContextPoolSample
{
    public class BooksController
    {
        private readonly BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        public Task CreateDatabaseAsync() => _booksService.CreateDatabaseAsync();
        public Task AddBooksAsync() => _booksService.AddBooksAsync();
        public Task ReadBooksAsync() => _booksService.ReadBooksAsync();
    }
}
