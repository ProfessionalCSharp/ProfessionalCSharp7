using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RelationUsingConventions
{
    class Program
    {
        static void Main()
        {
            //CreateDatabase();
            //AddBooks();
            EagerLoading();
            ExplicitLoading();
//            DeleteDatabase();
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

        private static void EagerLoading()
        {
            Console.WriteLine(nameof(EagerLoading));
            using (var context = new BooksContext())
            {
                var book = context.Books.Include(b => b.Chapters).Where(b => b.Title.StartsWith("Professional C# 7")).FirstOrDefault();
                if (book != null)
                {
                    Console.WriteLine(book.Title);

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
            using (var context = new BooksContext())
            {
                context.Database.EnsureDeleted();
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
                var reviewer = new User
                {
                    Name = "Istvan Novak"
                };
                var editor = new User
                {
                    Name = "Charlotte Kughen"
                };
                var b1 = new Book
                {
                    Title = "Professional C# 7 and .NET Core 2.0",
                    //ProjectEditor = editor,
                    //Reviewer = reviewer,
                    Author = author
                };

                var c1 = new Chapter
                {
                    Title = ".NET Applications and Tools",
                    Number = 1
                };
                var c2 = new Chapter
                {
                    Title = "Core C#",
                    Number = 2
                };
                var c3 = new Chapter
                {
                    Title = "Entity Framework Core",
                    Number = 28
                };

                b1.Chapters.AddRange(new[] { c1, c2, c3 });
                context.Books.Add(b1);
                context.Users.AddRange(author, editor, reviewer);
                context.Chapters.AddRange(c1, c2, c3);

                int records = context.SaveChanges();

                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }
    }
}
