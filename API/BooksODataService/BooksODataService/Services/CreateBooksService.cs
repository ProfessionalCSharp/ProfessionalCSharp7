using BooksODataService.Models;
using System.Collections.Generic;
using System.Linq;

namespace BooksODataService.Services
{
    public class CreateBooksService
    {
        private readonly BooksContext _booksContext;
        public CreateBooksService(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public void CreateDatabase()
        {
            bool created = _booksContext.Database.EnsureCreated();
            if (created)
            {
                CreateSampleBooks();
            }
        }

        private string[][] _chapterTitles = new[]
        {
            new []
            {
               ".NET Applications and Tools",
               "Core C#",
               "Objects and Types",
               "Object-Oriented Programming with C#",
               "Generics",
               "Operators and Casts",
               "Arrays",
               "Delegates, Lambdas, and Events",
               "Strings and Regular Expressions",
               "Collections",
               "Special Collections",
               "Language Integrated Query",
               "Functional Programming with C#",
               "Errors and Exceptions",
               "Asynchronous Programming",
               "Reflection, Metadata, and Dynamic Programming",
               "Managed and Unmanaged Memory",
               "Visual Studio 2017",
               "Libraries, Assemblies, Packages, and NuGet",
               "Dependency Injection",
               "Tasks and Parallel Programming",
               "Files and Streams",
               "Networking",
               "Security",
               "Composition",
               "XML and JSON",
               "ADO.NET and Transactions",
               "Entity Framework Core",
               "Localization",
               "Testing",
               "Tracing, Logging, and Analytics",
               "ASP.NET Core",
               "ASP.NET Core MVC",
                "Web API",
                "SignalR and WebHooks",
                "Bots and Cognitive Services",
                "Windows Apps",
                "Patterns with XAML Apps",
                "Styling Windows Apps",
                "Advanced Windows Apps",
                "Xamarin"
            },
            new []
            {
               ".NET Applications Architectures",
               "Core C#",
               "Objects and Types",
               "Inheritance",
               "Managed and Unmanaged Resources",
               "Generics",
               "Arrays and Tuples",
               "Operators and Casts",
               "Delegates, Lambdas, and Events",
               "Strings and Regular Expressions",
               "Collections",
               "Special Collections",
               "Language Integrated Query",
               "Errors and Exceptions",
               "Asynchronous Programming",
               "Reflection, Metadata, and Dynamic Programming",
               "Visual Studio 2015",
               ".NET Compiler Platform",
               "Testing",
               "Diagnostics and Application Insights",
               "Tasks and Parallel Programming",
               "Task Synchronization",
               "Files and Streams",
               "Security",
               "Networking",
               "Composition",
               "XML and JSON",
               "Localization",
               "Core XAML",
               "Styling XAML Apps",
               "Patterns with XAML Apps",
                "Windows Apps: User Interfaces",
                "Advanced Windows Apps",
                "Windows Desktop Applications with WPF",
                "Creating Documents with WPF",
                "Deploying Windows Apps",
                "ADO.NET",
               "Entity Framework Core",
               "Windows Services",
               "ASP.NET Core",
               "ASP.NET MVC",
                "ASP.NET Web API",
                "WebHooks and SignalR",
                "Windows Communication Foundation",
                "Deploying Websites and Services"
            },
           new []
            {
               ".NET Architecture",
               "Core C#",
               "Objects and Types",
               "Inheritance",
               "Generics",
               "Arrays and Tuples",
               "Operators and Casts",
               "Delegates, Lambdas, and Events",
               "Strings and Regular Expressions",
               "Collections",
               "Language Integrated Query",
               "Dynamic Language Extensions",
               "Asynchronous Programming",
               "Memory MAnagement and Pointers",
               "Reflection",
               "Errors and Exceptions",
               "Visual Studio 2013",
               "Deployment",
               "Assemblies",
               "Diagnostics",
               "Tasks, Threads, and Synchronization",
               "Security",
               "Interop",
               "Manipulating Files and the Registry",
               "Transactions",
               "Networking",
               "Windows Services",
               "Localization",
               "Core XAML",
               "Managed Extensibility Framework",
               "Windows Runtime",
               "Core ADP.NET",
               "ADO.NET Entity Framework",
               "Manipulating XML",
               "Core WPF",
               "Business Applications with WPF",
                "Creating Documents with WPF",
                "Windows Store Apps: User Interface",
                "Windows Store Apps: Contracts and Devices",
               "XML and JSON",
               "Core ASP.NET",
               "ASP.NET Web Forms",
               "ASP.NET MVC",
               "Windows Communication Foundation",
               "ASP.NET Web API",
               "Windows Workflow Foundation",
               "Peer-to-peer Networking",
               "Message Queuing"
            },
        };

        private string[] _bookTitles = new[]
        {
            "Professional C# 7 and .NET Core 2",
            "Professional C# 6 and .NET Core 1.0",
            "Professional C# 5 and .NET 4.5.1"
        };

        private string[] _bookIsbns = new[]
        {
            "978-1-119-44927-0",
            "978-1-119-09660-3",
            "978-1-118-83303-2"
        };

        private void CreateSampleBooks()
        {
            for (int i = 0; i < _bookTitles.Length; i++)
            {
                var b = new Book
                {
                    Title = _bookTitles[i],
                    Isbn = _bookIsbns[i],
                    Publisher = "Wrox Press"
                };
                var chapters = GetChapters(i, b);
                _booksContext.Chapters.AddRange(chapters);
                _booksContext.Books.Add(b);
            }
            int recordsChanged = _booksContext.SaveChanges();
        }

        private IEnumerable<BookChapter> GetChapters(int ix, Book book) =>
            _chapterTitles[ix].Select((title, n) => new BookChapter
            {
                Title = title,
                Book = book,
                Number = n + 1
            }).ToList();
    }
}
