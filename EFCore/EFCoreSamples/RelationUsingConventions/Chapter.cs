using System;
using System.Collections.Generic;
using System.Text;

namespace RelationUsingConventions
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
