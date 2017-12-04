using BooksServiceSample.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BooksServiceSample.Services
{
    public class BookChaptersService : IBookChaptersService
    {
        private readonly ConcurrentDictionary<Guid, Chapter> _chapters = new ConcurrentDictionary<Guid, Chapter>();

        public void Add(Chapter chapter)
        {
            //chapter.Id = Guid.NewGuid();
            //_chapters[chapter.Id] = chapter;
        }
        public void AddRange(IEnumerable<Chapter> chapters)
        {
            //foreach (var chapter in chapters)
            //{
            //    chapter.Id = Guid.NewGuid();
            //    _chapters[chapter.Id] = chapter;
            //}
        }
        public Chapter Find(Guid id)
        {
            _chapters.TryGetValue(id, out Chapter chapter);
            return chapter;
        }
        public IEnumerable<Chapter> GetAll() => _chapters.Values;
        public Chapter Remove(Guid id)
        {
            Chapter removed;
            _chapters.TryRemove(id, out removed);
            return removed;
        }

        public void Update(Chapter bookChapter)
        {
            throw new NotImplementedException();
        }
        //public void Update(Chapter chapter) =>
        //    _chapters[chapter.Id] = chapter;
    }
}
