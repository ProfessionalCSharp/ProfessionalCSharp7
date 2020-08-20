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
        static void Main()
        {
            
        }

        private Book? _book;
        public Book GetBook1()
        {
            if (_book == null)
            {
                _book = new Book("4711", "Professional C# 9");
            }
            return _book;
        }

        public Book GetBook2()
        {
            return _book ?? (_book = new Book("4711", "Professional C# 9"));
        }

        public Book GetBook3()
        {
            return _book ??= new Book("4711", "Professional C# 9");
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

        public string? GetPublisher2(Book book) => book.Publisher;

        public string GetPublisher1(Book? book) => book?.Publisher ?? string.Empty;

    }
}
