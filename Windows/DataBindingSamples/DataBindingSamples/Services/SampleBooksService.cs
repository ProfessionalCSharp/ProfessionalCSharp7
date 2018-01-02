using DataBindingSamples.Models;
using System.Collections.Generic;

namespace DataBindingSamples.Services
{
    public class SampleBooksService
    {
        public IEnumerable<Book> GetSampleBooks() =>
            new List<Book>()
            {
                new Book { BookId = 1, Title = "Professional C# 7 and .NET Core 2", Publisher = "Wrox Press" },
                new Book { BookId = 2, Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press" },
                new Book { BookId = 3, Title = "Professional C# 5.0 and .NET 4.5.1", Publisher = "Wrox Press" },
                new Book { BookId = 4, Title = "Enterprise Services with the .NET Framework", Publisher = "AWL" }
            };
    }
}
