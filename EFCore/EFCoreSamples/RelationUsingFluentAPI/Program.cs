using Microsoft.EntityFrameworkCore;
using System;

namespace RelationUsingFluentAPI
{
    class Program
    {
        static void Main()
        {
            CreateDatabase();
            AddBooks();
            GetBooksByUsers();
            DeleteDatabase();
        }

        private static void GetBooksByUsers()
        {
            using (var context = new BooksContext())
            {
                var users = context.Users.Include(u => u.WrittenBooks).Include(u => u.ReviewedBooks).Include(u => u.EditedBooks);

                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                    foreach (var book in user.WrittenBooks)
                    {
                        Console.WriteLine($"\twritten: {book.Title}");
                    }
                    foreach (var book in user.ReviewedBooks)
                    {
                        Console.WriteLine($"\treviewed: {book.Title}");
                    }
                    foreach (var book in user.EditedBooks)
                    {
                        Console.WriteLine($"\tedited: {book.Title}");
                    }
                }
            }
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
                context.Database.EnsureCreated();
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
                    Editor

                    = editor,
                    Reviewer = reviewer,
                    Author = author
                };

                var c1 = new Chapter
                {
                    Title = ".NET Applications and Tools",
                    Number = 1,
                    Book = b1
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
                context.Users.AddRange(author, editor, reviewer);
                context.Chapters.AddRange(c1, c2, c3);

                int records = context.SaveChanges();

                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }
    }
}
