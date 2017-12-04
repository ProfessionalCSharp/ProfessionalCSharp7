using BooksServiceSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookServices.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
