using BookServiceClientApp.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookServiceClientApp.Services
{
    public class BookChapterClientService : HttpClientService<BookChapter>
    {
        public BookChapterClientService(UrlService urlService, ILogger<BookChapterClientService> logger)
        : base(urlService, logger) { }
        public override async Task<IEnumerable<BookChapter>> GetAllAsync(string requestUri)
        {
            IEnumerable<BookChapter> chapters = await base.GetAllAsync(requestUri);
            return chapters.OrderBy(c => c.Number);
        }
    }

}
