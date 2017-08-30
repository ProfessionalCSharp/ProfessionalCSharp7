using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RelationUsingAnnotations
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; set; }

        public int AuthorUserId { get; set; }
        [ForeignKey("AuthorUserId")]
        public User Author { get; set; }
        public int ReviewerUserId { get; set; }
        [ForeignKey("ReviewerUserId")]
        public User Reviewer { get; set; }
    }
}
