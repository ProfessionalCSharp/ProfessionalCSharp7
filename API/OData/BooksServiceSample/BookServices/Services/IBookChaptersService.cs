using BooksServiceSample.Models;
using System;
using System.Collections.Generic;

namespace BooksServiceSample.Services
{
    public interface IBookChaptersService
    {
        void Add(Chapter bookChapter);
        void AddRange(IEnumerable<Chapter> chapters);
        IEnumerable<Chapter> GetAll();
        Chapter Find(Guid id);
        Chapter Remove(Guid id);
        void Update(Chapter bookChapter);
    }
}
