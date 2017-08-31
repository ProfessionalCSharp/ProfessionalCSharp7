using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RelationUsingAnnotations
{
    public class User
    {
        public int UserId { get; set; }
        [InverseProperty("Author")]
        public List<Book> AuthoredBooks { get; set; }
        [InverseProperty("Reviewer")]
        public List<Book> ReviewedBooks { get; set; }
    }
}
