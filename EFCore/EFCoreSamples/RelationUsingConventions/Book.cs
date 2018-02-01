using System.Collections.Generic;

namespace RelationUsingConventions
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; } = new List<Chapter>();

        public User Author { get; set; }
    }
}
