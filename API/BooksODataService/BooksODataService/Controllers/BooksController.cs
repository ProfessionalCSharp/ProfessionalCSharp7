using BooksODataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooksODataService.Controllers
{
    public class BooksController : ODataController
    {
        private readonly BooksContext _booksContext;
        public BooksController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Book> Get(ODataQueryOptions options)
        {
            ODataValidationSettings settings = new ODataValidationSettings()
            {
                MaxExpansionDepth = 4
            };
            options.Validate(settings);
            var books = _booksContext.Books.Include(b => b.Chapters);
            return books;
        }

        [EnableQuery()]
        public SingleResult<Book> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_booksContext.Books.Where(b => b.Id == key));
        }

    }

}
