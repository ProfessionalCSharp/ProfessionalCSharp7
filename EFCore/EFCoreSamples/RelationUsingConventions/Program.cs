using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RelationUsingConventions
{
    class Program
    {
        static void Main()
        {
            AddLogging();
            CreateDatabase();
            AddBooks();
            NoImplicitLoadingWithEFCore2();  // Entity Framework 6 does support implicit loading
            ExplicitLoading();
            EagerLoading();
            BooksForAuthor();
            DeleteDatabase();
        }

        private static void NoImplicitLoadingWithEFCore2()
        {
            Console.WriteLine(nameof(NoImplicitLoadingWithEFCore2));
            using (var context = new BooksContext())
            {
                var book1 = (from b in context.Books
                             from c in b.Chapters
                             where c.Title.StartsWith("Entity")
                             select b).FirstOrDefault();

                var book = context.Books
                    .SelectMany(b => b.Chapters, (b, chapters) => new { Book = b, Chapters = chapters })  // defining expression trees does not support tuples (yet)
                    .Where(bc => bc.Chapters.Title.StartsWith("Entity"))
                    .Select(bc => bc.Book).FirstOrDefault();
                //var book = context.Books
                //    .SelectMany(b => b.Chapters, (b, chapters) => (Book: b, Chapters: chapters))  // defining expression trees does not support tuples (yet) - see https://github.com/dotnet/roslyn/issues/12897
                //    .Where(bc => bc.Chapters.Title.StartsWith("Entity"))
                //    .Select(bc => bc.Book).FirstOrDefault();

                if (book != null && book.Chapters == null)
                {
                    Console.WriteLine("Chapters are not implicitly loaded with EF Core, this is different from Entity Framework");
                }
              

                if (book != null)
                {
                    Console.WriteLine(book.Title);
                    if (!context.Entry(book).Collection(b => b.Chapters).IsLoaded)
                    {
                        context.Entry(book).Collection(b => b.Chapters).Load();
                    }

                    foreach (var chapter in book.Chapters)
                    {
                        Console.WriteLine($"{chapter.Number}. {chapter.Title}");
                    }
                }
            }
            Console.WriteLine();
        }

        private static void BooksForAuthor()
        {
            using (var context = new BooksContext())
            {
                var author = context.Users.Include(u => u.AuthoredBooks).Where(u => u.Name == "Christian Nagel").FirstOrDefault();
                if (author != null)
                {
                    Console.WriteLine(author.Name);
                    foreach (var b in author.AuthoredBooks)
                    {
                        Console.WriteLine(b.Title);
                    }
                }
            }
        }

        private static void ExplicitLoading()
        {
            Console.WriteLine(nameof(ExplicitLoading));
            using (var context = new BooksContext())
            {
                var book = context.Books.Where(b => b.Title.StartsWith("Professional C# 7")).FirstOrDefault();
                if (book != null)
                {
                    Console.WriteLine(book.Title);
                    context.Entry(book).Collection(b => b.Chapters).Load();
                    context.Entry(book).Reference(b => b.Author).Load();
                    Console.WriteLine(book.Author.Name);
                    foreach (var chapter in book.Chapters)
                    {
                        Console.WriteLine($"{chapter.Number}. {chapter.Title}");
                    }
                }
            }
            Console.WriteLine();
        }

        private static void EagerLoading()
        {
            Console.WriteLine(nameof(EagerLoading));
            using (var context = new BooksContext())
            {
                var book = context.Books
                    .Include(b => b.Chapters)
                    .Include(b => b.Author)
                    .Where(b => b.Title.StartsWith("Professional C# 7"))
                    .FirstOrDefault();
                if (book != null)
                {
                    Console.WriteLine(book.Title);

                    Console.WriteLine(book.Author.Name);
                    foreach (var chapter in book.Chapters)
                    {
                        Console.WriteLine($"{chapter.Number}. {chapter.Title}");
                    }
                }
            }
            Console.WriteLine();
        }

        private static void DeleteDatabase()
        {
            Console.Write("Delete the database? ");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                using (var context = new BooksContext())
                {
                    context.Database.EnsureDeleted();
                }
            }
        }

        private static void CreateDatabase()
        {
            using (var context = new BooksContext())
            {
                bool created = context.Database.EnsureCreated();
                Console.WriteLine($"database created: {created}");
            }
        }

        private static void AddBooks()
        {
            Console.WriteLine(nameof(AddBooks));
            using (var context = new BooksContext())
            {
                var author = new User
                {
                    Name = "Christian Nagel"
                };
                var b1 = new Book
                {
                    Title = "Professional C# 7 and .NET Core 2.0",
                    Author = author
                };

                var c1 = new Chapter
                {
                    Title = ".NET Applications and Tools",
                    Number = 1,
                    Book = b1,
                };
                var c2 = new Chapter
                {
                    Title = "Core C#",
                    Number = 2,
                    Book = b1
                };
                var c3 = new Chapter
                {
                    Title = "Entity Framework Core",
                    Number = 28,
                    Book = b1
                };

                context.Books.Add(b1);

                context.Users.Add(author);
                context.Chapters.AddRange(c1, c2, c3);

                int records = context.SaveChanges();
                b1.Chapters.AddRange(new[] { c1, c2, c3 });

                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        private static void AddLogging()
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
