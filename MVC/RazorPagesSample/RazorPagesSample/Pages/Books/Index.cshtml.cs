using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesSample.Models;

namespace RazorPagesSample.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesSample.Models.BooksContext _context;

        public IndexModel(RazorPagesSample.Models.BooksContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }

        public async Task OnGetAsync()
        {
            Book = await _context.Books.ToListAsync();
        }
    }
}
