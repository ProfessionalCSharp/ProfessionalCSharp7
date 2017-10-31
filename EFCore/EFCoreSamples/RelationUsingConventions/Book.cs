using System.Collections.Generic;

namespace RelationUsingConventions
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; } = new List<Chapter>();

        public int AuthorId { get; set; }
        public User Author { get; set; }
       // public int? ReviewerId { get; set; }
       // public User Reviewer { get; set; }
       //public int? ProjectEditorId { get; set; }
       // public User ProjectEditor { get; set; }
    }
}
