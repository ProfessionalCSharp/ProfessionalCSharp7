using DataBindingSamples.Models;
using System.Collections.Generic;

namespace DataBindingSamples.Services
{
    public class SampleBooksService
    {
        public IEnumerable<Book> GetSampleBooks() =>
            new List<Book>()
            {
                new Book(1, "Professional C# 7 and .NET Core 2", "Wrox Press", "Christian Nagel"),
                new Book(2, "Professional C# 6 and .NET Core 1.0", "Wrox Press", "Christian Nagel"),
                new Book(3, "Professional C# 5.0 and .NET 4.5.1", "Wrox Press", "Christian Nagel", "Jay Glynn", "Morgan Skinner"),
                new Book(4, "Enterprise Services with the .NET Framework", "AWL", "Christian Nagel")
            };
    }
}
