using System;

namespace NullableSampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Professional C#");
            // book.Title = "Professional C#";
        }

        static void ShowBook(Book book)
        {
            Console.WriteLine($"{book.Title.ToUpper()} {book.Publisher?.ToUpper()}");
        }
    }
}
