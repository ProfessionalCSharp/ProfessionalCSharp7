using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MappingToFields.ColumnNames;

namespace MappingToFields
{
    class Program
    {
        static async Task Main()
        {
            var p = new Program();
            await p.CreateTheDatabaseAsync();
            await p.AddBookAsync("Professional C# 7", "Wrox Press");
            await p.AddBookAsync("Test", "Test");
            await p.AddBooksAsync();
            await p.ReadBooksAsync();
            await p.QueryBooksAsync();
            await p.DeleteBookAsync(2);
            await p.QueryDeletedBooksAsync();

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
            using (var context = new BooksContext())
            {
                bool deleted = await context.Database.EnsureDeletedAsync();
                string deletionInfo = deleted ? "deleted" : "not deleted";
                Console.WriteLine($"database {deletionInfo}");
            }
        }

        private async Task AddBookAsync(string title, string publisher)
        {
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
            using (var context = new BooksContext())
            {
                var b1 = new Book("Professional C# 6 and .NET Core 1.0", "Wrox Press");
                var b2 = new Book("Professional C# 5 and .NET 4.5.1", "Wrox Press");
                var b3 = new Book("JavaScript for Kids", "Wrox Press");
                var b4 = new Book("Web Design with HTML and CSS", "For Dummies");
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
                    Console.WriteLine(b);
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
    
        private async Task UpdateBookAsync(Book updateBook)
        {
            using (var context = new BooksContext())
            {
                context.Entry<Book>(updateBook).State = EntityState.Modified;
                await context.SaveChangesAsync();
            } 
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

        private async Task QueryDeletedBooksAsync()
        {
            using (var context = new BooksContext())
            {
                IEnumerable<Book> deletedBooks = 
                    await context.Books
                    .Where(b => EF.Property<bool>(b, IsDeleted))
                    .ToListAsync();
                foreach (var book in deletedBooks)
                {
                    Console.WriteLine($"deleted: {book}");
                }
            }
        }
    }
}
