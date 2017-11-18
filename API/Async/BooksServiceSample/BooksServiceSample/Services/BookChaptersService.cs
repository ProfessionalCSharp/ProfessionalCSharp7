using BooksServiceSample.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksServiceSample.Services
{
    public class BookChaptersService : IBookChaptersService
    {
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters = new ConcurrentDictionary<Guid, BookChapter>();

        public Task AddAsync(BookChapter chapter)
        {
            chapter.Id = Guid.NewGuid();
            _chapters[chapter.Id] = chapter;
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            foreach (var chapter in chapters)
            {
                chapter.Id = Guid.NewGuid();
                _chapters[chapter.Id] = chapter;
            }
            return Task.CompletedTask;
        }

        public Task<BookChapter> RemoveAsync(Guid id)
        {
            _chapters.TryRemove(id, out BookChapter removed);
            return Task.FromResult(removed);
        }

        public Task<IEnumerable<BookChapter>> GetAllAsync() =>
          Task.FromResult<IEnumerable<BookChapter>>(_chapters.Values);

        public Task<BookChapter> FindAsync(Guid id)
        {
            _chapters.TryGetValue(id, out BookChapter chapter);
            return Task.FromResult(chapter);
        }

        public Task UpdateAsync(BookChapter chapter)
        {
            _chapters[chapter.Id] = chapter;
            return Task.CompletedTask;
        }
    }
}
