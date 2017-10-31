using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationUsingAnnotations
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; } = new List<Chapter>();

        public int? AuthorUserId { get; set; }
        [ForeignKey(nameof(AuthorUserId))]
        public User Author { get; set; }
        public int? ReviewerUserId { get; set; }
        [ForeignKey(nameof(ReviewerUserId))]
        public User Reviewer { get; set; }
        public int? ProjectEditorId { get; set; }
        [ForeignKey(nameof(ProjectEditorId))]
        public User ProjectEditor { get; set; }
    }
}
