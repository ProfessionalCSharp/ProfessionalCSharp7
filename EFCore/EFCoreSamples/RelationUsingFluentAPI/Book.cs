using System;
using System.Collections.Generic;
using System.Text;

namespace RelationUsingFluentAPI
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; set; }

        public int AuthorUserId { get; set; }
        public User Author { get; set; }
        public int ReviewerUserId { get; set; }
        public User Reviewer { get; set; }
    }
}
