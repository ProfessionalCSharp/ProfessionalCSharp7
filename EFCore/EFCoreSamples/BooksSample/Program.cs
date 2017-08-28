using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BooksSample.ColumnNames;

namespace BooksSample
{
    class Program
    {
        static async Task Main()
        {
            var p = new Program();
            p.AddLogging();
            await p.CreateTheDatabaseAsync();
            await p.AddBookAsync("Professional C# 7", "Wrox Press");
            await p.AddBookAsync("Test", "Test");
            await p.AddBooksAsync();
            await p.QueryAllBooksAsync();
            await p.QueryBookByKeyAsync(2);
            await p.UpdateBookAsync();
            await p.QueryBookAsync("Professional C# 7 and .NET Core 2.0");
            await p.FilterBooksAsync("Pro");

            await p.DeleteBookAsync(2);
            await p.QueryDeletedBooksAsync();
            await p.QueryBooksAsync();
            await p.ClientAndServerEvaluationAsync();
            await p.RawSqlQuery("Wrox Press");

            await p.DeleteDatabaseAsync();
        }


        private async Task CreateTheDatabaseAsync()
        {
            Console.WriteLine(nameof(CreateTheDatabaseAsync));
            using (var context = new BooksContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();
                string creationInfo = created ? "created" : "exists";
                Console.WriteLine($"database {creationInfo}");
            }
            Console.WriteLine();
        }

        private async Task DeleteDatabaseAsync()
        {
            Console.WriteLine(nameof(DeleteDatabaseAsync));
            using (var context = new BooksContext())
            {
                bool deleted = await context.Database.EnsureDeletedAsync();
                string deletionInfo = deleted ? "deleted" : "not deleted";
                Console.WriteLine($"database {deletionInfo}");
            }
            Console.WriteLine();
        }

        private async Task AddBookAsync(string title, string publisher)
        {
            Console.WriteLine(nameof(AddBookAsync));
            using (var context = new BooksContext())
            {
                var book = new Book(title, publisher);
                await context.Books.AddAsync(book);
                int records = await context.SaveChangesAsync();

                Console.WriteLine($"{records} record added");
            }
            Console.WriteLine();
        }

        private async Task AddBooksAsync()
        {
            Console.WriteLine(nameof(AddBooksAsync));
            using (var context = new BooksContext())
            {
                var b1 = new Book("Professional C# 6 and .NET Core 1.0", "Wrox Press");
                var b2 = new Book("Professional C# 5 and .NET 4.5.1", "Wrox Press");
                var b3 = new Book("JavaScript for Kids", "Wrox Press");
                var b4 = new Book("HTML and CSS", "John Wiley");
                await context.Books.AddRangeAsync(b1, b2, b3, b4);

                var a1 = new Author("Christian", "Nagel");
                var a2 = new Author("Jay", "Glynn");
                var a3 = new Author("Jon", "Duckett");
                var a4 = new Author("Nick", "Morgan");
                await context.Authors.AddRangeAsync(a1, a2, a3, a4);

                var ba1 = new BookAuthor { Author = a1, Book = b1 };
                var ba2 = new BookAuthor { Author = a1, Book = b2 };
                var ba3 = new BookAuthor { Author = a2, Book = b2 };
                var ba4 = new BookAuthor { Author = a3, Book = b4 };
                var ba5 = new BookAuthor { Author = a4, Book = b3 };
                await context.BookAuthors.AddRangeAsync(ba1, ba2, ba3, ba4, ba5);

                int records = await context.SaveChangesAsync();

                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        private async Task UpdateBookAsync()
        {
            using (var context = new BooksContext())
            {
                int records = 0;
                Book book = await context.Books
                    .Where(b => b.Title == "Professional C# 7")
                    .FirstOrDefaultAsync();
                if (book != null)
                {
                    book.Title = "Professional C# 7 and .NET Core 2.0";
                    records = await context.SaveChangesAsync();
                }

                Console.WriteLine($"{records} record updated");
            }
            Console.WriteLine();
        }

        private async Task QueryAllBooksAsync()
        {
            Console.WriteLine(nameof(QueryAllBooksAsync));
            using (var context = new BooksContext())
            {
                List<Book> books = await context.Books.ToListAsync();
                foreach (var b in books)
                {
                    Console.WriteLine(b);
                }
            }
            Console.WriteLine();
        }

        public async Task QueryBookByKeyAsync(int id)
        {
            Console.WriteLine(nameof(QueryBookByKeyAsync));
            using (var context = new BooksContext())
            {
                Book b = await context.Books.FindAsync(id);
                if (b != null)
                {
                    Console.WriteLine($"found book {b}");
                }
            }
            Console.WriteLine();
        }

        private async Task QueryBooksAsync()
        {
            using (var context = new BooksContext())
            {
                List<Book> wroxBooks = await context.Books
                    .Where(b => b.Publisher == "Wrox Press")
                    .ToListAsync();

                foreach (var b in wroxBooks)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
                }
            }
            Console.WriteLine();
        }

        public async Task QueryBookAsync(string title)
        {
            Console.WriteLine(nameof(QueryBookAsync));
            using (var context = new BooksContext())
            {
                Book book = await context.Books.SingleOrDefaultAsync(b => b.Title == title);
                if (book != null)
                {
                    Console.WriteLine($"found book {book}");
                }
            }
            Console.WriteLine();
        }


        private async Task FilterBooksAsync(string title)
        {
            Console.WriteLine(nameof(FilterBooksAsync));
            using (var context = new BooksContext())
            {
                List<Book> wroxBooks = await context.Books
                    .Where(b => b.Title.Contains(title))
                    .ToListAsync();

                foreach (var b in wroxBooks)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
                }
            }
            Console.WriteLine();
        }

        private async Task ClientAndServerEvaluationAsync()
        {
            Console.WriteLine(nameof(ClientAndServerEvaluationAsync));
            string textQuery = ".net";
            using (var context = new BooksContext())
            {
                List<Book> wroxBooks = await context.Books
                    .Where(b => b.Title.Contains(textQuery.ToUpper()))
                    .ToListAsync();

                foreach (var b in wroxBooks)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
                }
            }
            Console.WriteLine();
        }

        private async Task DeleteBookAsync(int id)
        {
            using (var context = new BooksContext())
            {
                Book b = await context.Books.FindAsync(id);
                if (b == null) return;

                context.Books.Remove(b);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} books deleted");
            }
            Console.WriteLine();
        }

        // this method only returns the deleted book if the global query filter
        // is removed in the BooksContext class
        private async Task QueryDeletedBooksAsync()
        {
            using (var context = new BooksContext())
            {
                IEnumerable<Book> deletedBooks =
                    await context.Books
                    .Where(b => EF.Property<bool>(b, IsDeleted))
                    .ToListAsync();
                //IEnumerable<Book> deletedBooks =
                //await context.Books.FromSql($"SELECT * FROM Books WHERE IsDeleted = 1").ToListAsync();
                foreach (var book in deletedBooks)
                {
                    Console.WriteLine($"deleted: {book}");
                }
            }
        }

        private async Task RawSqlQuery(string publisher)
        {
            Console.WriteLine(nameof(RawSqlQuery));
            using (var context = new BooksContext())
            {
                IList<Book> books = await context.Books.FromSql($"SELECT * FROM Books WHERE Publisher = {publisher}").ToListAsync();

                foreach (var b in books)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
                }
            }
            Console.WriteLine();
        }

        private async Task CompiledQueryAsync()
        {
            return;
        }

        private void AddLogging()
        {
            using (var context = new BooksContext())
            {
                IServiceProvider provider = context.GetInfrastructure<IServiceProvider>();
                ILoggerFactory loggerFactory = provider.GetService<ILoggerFactory>();
                loggerFactory.AddConsole(LogLevel.Information);
            }
        }
    }
}
