using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BooksServiceSample.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksServiceSample.Services
{
    public class DBBookChaptersService : IBookChaptersService
    {
        private readonly BooksContext _booksContext;
        public DBBookChaptersService(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }
        public async Task AddAsync(BookChapter chapter)
        {
            await _booksContext.Chapters.AddAsync(chapter);
            await _booksContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            await _booksContext.Chapters.AddRangeAsync(chapters);
            await _booksContext.SaveChangesAsync();
        }

        public Task<BookChapter> FindAsync(Guid id) =>
            _booksContext.Chapters.FindAsync(id);

        public async Task<IEnumerable<BookChapter>> GetAllAsync() =>
            await _booksContext.Chapters.ToListAsync();

        public async Task<BookChapter> RemoveAsync(Guid id)
        {
            BookChapter chapter = await _booksContext.Chapters.SingleOrDefaultAsync(c => c.Id == id);
            if (chapter == null) return null;

            _booksContext.Chapters.Remove(chapter);
            await _booksContext.SaveChangesAsync();
            return chapter;
        }

        public async Task UpdateAsync(BookChapter chapter)
        {
            _booksContext.Chapters.Update(chapter);
            await _booksContext.SaveChangesAsync();
        }
    }
}
