using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RelationUsingFluentAPI
{
    public class User
    {
        public int UserId { get; set; }
        [InverseProperty("Author")]
        public List<Book> Books { get; set; }
        [InverseProperty("Reviewer")]
        public List<Book> ReviewedBooks { get; set; }
    }
}
