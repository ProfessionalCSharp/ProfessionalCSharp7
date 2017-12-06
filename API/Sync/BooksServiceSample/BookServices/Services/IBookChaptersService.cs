using BooksServiceSample.Models;
using System;
using System.Collections.Generic;

namespace BooksServiceSample.Services
{
    public interface IBookChaptersService
    {
        void Add(BookChapter bookChapter);
        void AddRange(IEnumerable<BookChapter> chapters);
        IEnumerable<BookChapter> GetAll();
        BookChapter Find(Guid id);
        BookChapter Remove(Guid id);
        void Update(BookChapter bookChapter);
    }
}
