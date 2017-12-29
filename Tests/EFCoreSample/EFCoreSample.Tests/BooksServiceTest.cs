using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EFCoreSample
{
    public class BooksServiceTest : IDisposable
    {
        private BooksContext _booksContext;
        private const string PublisherName = "A Publisher";
        public BooksServiceTest()
        {
            InitContext();
        }

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<BooksContext>().UseInMemoryDatabase("BooksDB");
            _booksContext = new BooksContext(builder.Options);

            // init with 1000 books
            var books = Enumerable.Range(1, 1000).Select(i => new Book { BookId = i, Title = $"Sample {i}", Publisher = PublisherName }).ToList();
            _booksContext.Books.AddRange(books);
            _booksContext.SaveChanges();
        }

        [Fact]
        public void GetTopBooksByPublisherCount()
        {
            // arrange
            var booksService = new BooksService(_booksContext);
            // act
            var topbooks = booksService.GetTopBooksByPublisher(PublisherName);
            // assert
            Assert.Equal(10, topbooks.Count());
        }

        public void Dispose()
        {
            _booksContext?.Dispose();
        }
    }
}
