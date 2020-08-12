using System;

namespace NullableSampleApp
{
    class Test : BindableBase
    {
        private string _first = string.Empty;

        public string First
        {
            get => _first;
            set => SetProperty(ref _first, value);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
        }

        static void ShowBook(Book book)
        {
            Console.WriteLine($"{book.Title.ToUpper()} {book.Publisher?.ToUpper()}");
        }

        static void AssignNullableToNonNullable(Book b)
        {
            // string publisher1 = b.Publisher; // compiler warning
            if (b.Publisher != null)
            {
                string publisher2 = b.Publisher; // ok
            }

            string publisher3 = b.Publisher ?? string.Empty;
            string? publisher4 = b.Publisher;
        }

    }
}
