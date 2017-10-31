using System.ComponentModel.DataAnnotations.Schema;

namespace RelationUsingAnnotations
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
