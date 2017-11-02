using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationUsingAnnotations
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        [InverseProperty("Author")]
        public List<Book> WrittenBooks { get; set; }
        [InverseProperty("Reviewer")]
        public List<Book> ReviewedBooks { get; set; }
        [InverseProperty("ProjectEditor")]
        public List<Book> EditedBooks { get; set; }
    }
}
