using BooksServiceSample.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksServiceSample.Services
{
    public interface IBookChaptersService
    {
        Task AddAsync(BookChapter chapter);
        Task AddRangeAsync(IEnumerable<BookChapter> chapters);
        Task<BookChapter> RemoveAsync(Guid id);
        Task<IEnumerable<BookChapter>> GetAllAsync();
        Task<BookChapter> FindAsync(Guid id);
        Task UpdateAsync(BookChapter chapter);
    }
}
