using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSample.Models;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesSample.Pages
{
    public class CodeBehindModel : PageModel
    {
        private readonly BooksContext _context;
        public CodeBehindModel(BooksContext context) => _context = context;

        public void OnGet()
        {
            bool created = _context.Database.EnsureCreated();
            if (created) SeedBooks();

            Books = _context.Books.ToList();
        }

        public IEnumerable<Book> Books { get; set; }

        private void SeedBooks()
        {
            _context.Books.Add(new Book { Title = "Professional C# 6 and .NET Core 1", Publisher = "Wrox Press" });
            _context.Books.Add(new Book { Title = "Professional C# 7 and .NET Core 2", Publisher = "Wrox Press" });
            _context.SaveChanges();
        }

        public void OnPost()
        {
            _context.Books.Add(Book);
            _context.SaveChanges();
            Message = "Book saved";
            Books = _context.Books.ToList();
        }

        [BindProperty()]
        public Book Book { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}