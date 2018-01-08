using System.Collections.Generic;

namespace ShareSource.Models
{
    public class BooksRepository
    {
        public IEnumerable<Book> GetSampleBooks() =>
            new List<Book>()
            {
                new Book
                {
                    Title = "Professional C# 7 and .NET Core 2",
                    Publisher = "Wrox Press"
                },
                new Book
                {
                    Title = "Professional C# 6 and .NET Core 1.0",
                    Publisher = "Wrox Press"
                },
                new Book
                {
                    Title = "Professional C# 5.0 and .NET 4.5.1",
                    Publisher = "Wrox Press"
                }
            };
    }
}
