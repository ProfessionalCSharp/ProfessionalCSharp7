using System;
using System.Collections.Generic;
using System.Text;

namespace RelationUsingConventions
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
