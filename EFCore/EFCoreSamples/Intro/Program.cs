using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro
{
    class Program
    {
        static async Task Main()
        {
            var p = new Program();
            p.AddLogging();
            await p.CreateTheDatabaseAsync();
            await p.AddBookAsync("Professional C# 7", "Wrox Press");
            await p.AddBooksAsync();
            await p.ReadBooksAsync();
            await p.QueryBooksAsync();
            await p.UpdateBookAsync();

            await p.DeleteBooksAsync();
            await p.DeleteDatabaseAsync();
        }

        private async Task CreateTheDatabaseAsync()
        {
            using (var context = new BooksContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();
                string creationInfo = created ? "created" : "exists";
                Console.WriteLine($"database {creationInfo}");
            }
        }

        private async Task DeleteDatabaseAsync()
        {
            Console.Write("Delete the database? ");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                using (var context = new BooksContext())
                {
                    bool deleted = await context.Database.EnsureDeletedAsync();
                    string deletionInfo = deleted ? "deleted" : "not deleted";
                    Console.WriteLine($"database {deletionInfo}");
                }
            }
        }

        private async Task AddBookAsync(string title, string publisher)
        {
            using (var context = new BooksContext())
            {
                var book = new Book { Title = title, Publisher = publisher };
                await context.Books.AddAsync(book);
                int records = await context.SaveChangesAsync();

                Console.WriteLine($"{records} record added");
            }
            Console.WriteLine();
        }

        private async Task AddBooksAsync()
        {
            using (var context = new BooksContext())
            {
                var b1 = new Book { Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press" };
                var b2 = new Book { Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press" };
                var b3 = new Book { Title = "JavaScript for Kids", Publisher = "Wrox Press" };
                var b4 = new Book { Title = "Web Design with HTML and CSS", Publisher = "For Dummies" };
                await context.Books.AddRangeAsync(b1, b2, b3, b4);
                int records = await context.SaveChangesAsync();

                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        private async Task ReadBooksAsync()
        {
            using (var context = new BooksContext())
            {
                List<Book> books = await context.Books.ToListAsync();
                foreach (var b in books)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
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

                // comment the previous lines and uncomment the next lines to try the LINQ query syntax
                //var wroxBooks = await (from b in context.Books
                //                         where b.Publisher == "Wrox Press"
                //                         select b).ToListAsync();

                foreach (var b in wroxBooks)
                {
                    Console.WriteLine($"{b.Title} {b.Publisher}");
                }
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

        private async Task DeleteBooksAsync()
        {
            using (var context = new BooksContext())
            {
                List<Book> books = await context.Books.ToListAsync();
                context.Books.RemoveRange(books);
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records deleted");
            }
            Console.WriteLine();
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
