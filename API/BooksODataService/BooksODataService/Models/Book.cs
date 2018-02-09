using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksODataService.Models
{
    public class Book
    {
        public Book()
        {
            Chapters = new List<BookChapter>();
        }
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public List<BookChapter> Chapters { get; set; }
    }

}
