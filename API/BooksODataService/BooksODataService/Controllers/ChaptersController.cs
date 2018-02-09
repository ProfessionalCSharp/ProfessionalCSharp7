using BooksODataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksODataService.Controllers
{
    public class ChaptersController : ODataController
    {
        private readonly BooksContext _booksContext;
        public ChaptersController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public IQueryable<BookChapter> Get() =>
            _booksContext.Chapters.Include(c => c.Book);

        [EnableQuery]
        public SingleResult<BookChapter> Get([FromODataUri] int key) =>
            SingleResult.Create(_booksContext.Chapters.Where(c => c.Id == key));

    }
}
