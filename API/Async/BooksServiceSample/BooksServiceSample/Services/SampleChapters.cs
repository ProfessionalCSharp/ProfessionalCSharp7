using BooksServiceSample.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksServiceSample.Services
{
    public class SampleChapters
    {
        private readonly IBookChaptersService _bookChaptersService;
        public SampleChapters(IBookChaptersService bookChapterService)
        {
            _bookChaptersService = bookChapterService;
        }

        private string[] sampleTitles = new[]
        {
            ".NET Applications and Tools",
            "Core C#",
            "Objects and Types",
            "Object-Oriented Programming with C#",
            "Generics",
            "Operators and Casts",
            "Arrays",
            "Delegates, Lambdas, and Events"
        };
        private int[] numberPages = { 35, 42, 33, 20, 24, 38, 20, 32 };


        public async Task CreateSampleChaptersAsync()
        {
            var chapters = new List<BookChapter>();
            for (int i = 0; i < 8; i++)
            {
                chapters.Add(new BookChapter
                {
                    Number = i,
                    Title = sampleTitles[i],
                    Pages = numberPages[i]
                });
            }
           await _bookChaptersService.AddRangeAsync(chapters);
        }
    }
}
