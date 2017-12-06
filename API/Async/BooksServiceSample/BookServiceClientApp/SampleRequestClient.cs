using BookServiceClientApp.Models;
using BookServiceClientApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookServiceClientApp
{
    public class SampleRequestClient
    {
        private readonly UrlService _urlService;
        private readonly BookChapterClientService _client;
        public SampleRequestClient(UrlService urlService, BookChapterClientService client)
        {
            _urlService = urlService;
            _client = client;
        }

        public async Task ReadChaptersAsync()
        {
            Console.WriteLine(nameof(ReadChaptersAsync));
            IEnumerable<BookChapter> chapters = await _client.GetAllAsync(_urlService.BooksApi);
            foreach (BookChapter chapter in chapters)
            {
                Console.WriteLine(chapter.Title);
            }
            Console.WriteLine();
        }

        public async Task ReadChapterAsync()
        {
            Console.WriteLine(nameof(ReadChapterAsync));
            var chapters = await _client.GetAllAsync(_urlService.BooksApi);
            Guid id = chapters.First().Id;
            BookChapter chapter = await _client.GetAsync(_urlService.BooksApi + id);
            Console.WriteLine($"{chapter.Number} {chapter.Title}");
            Console.WriteLine();
        }

        public async Task ReadNotExistingChapterAsync()
        {
            Console.WriteLine(nameof(ReadNotExistingChapterAsync));
            string requestedIdentifier = Guid.NewGuid().ToString();
            try
            {
                BookChapter chapter = await _client.GetAsync(
                  _urlService.BooksApi + requestedIdentifier.ToString());
                Console.WriteLine($"{chapter.Number} {chapter.Title}");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                Console.WriteLine($"book chapter with the identifier " +
                  $"{requestedIdentifier} not found");
            }
            Console.WriteLine();
        }

        public async Task ReadXmlAsync()
        {
            Console.WriteLine(nameof(ReadXmlAsync));
            XElement chapters = await _client.GetAllXmlAsync(_urlService.BooksApi);
            Console.WriteLine(chapters);
            Console.WriteLine();
        }

        public async Task AddChapterAsync()
        {
            Console.WriteLine(nameof(AddChapterAsync));
            BookChapter chapter = new BookChapter
            {
                Number = 42,
                Title = "ASP.NET Web API",
                Pages = 35
            };
            chapter = await _client.PostAsync(_urlService.BooksApi, chapter);
            Console.WriteLine($"added chapter {chapter.Title} with id {chapter.Id}");
            Console.WriteLine();
        }

        public async Task UpdateChapterAsync()
        {
            Console.WriteLine(nameof(UpdateChapterAsync));
            var chapters = await _client.GetAllAsync(_urlService.BooksApi);
            var chapter = chapters.SingleOrDefault(c => c.Title == "Windows Store Apps");
            if (chapter != null)
            {
                chapter.Number = 32;
                chapter.Title = "Windows Apps";
                await _client.PutAsync(_urlService.BooksApi + chapter.Id, chapter);
                Console.WriteLine($"updated chapter {chapter.Title}");
            }
            Console.WriteLine();
        }

        public async Task RemoveChapterAsync()
        {
            Console.WriteLine(nameof(RemoveChapterAsync));
            var chapters = await _client.GetAllAsync(_urlService.BooksApi);
            var chapter = chapters.SingleOrDefault(c => c.Title == "ASP.NET Web Forms");
            if (chapter != null)
            {
                await _client.DeleteAsync(_urlService.BooksApi + chapter.Id);
                Console.WriteLine($"removed chapter {chapter.Title}");
            }
            Console.WriteLine();
        }
    }
}
