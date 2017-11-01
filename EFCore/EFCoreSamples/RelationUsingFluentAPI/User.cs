using System.Collections.Generic;

namespace RelationUsingFluentAPI
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Book> WrittenBooks { get; set; }
        public List<Book> ReviewedBooks { get; set; }
        public List<Book> EditedBooks { get; set; }
    }
}
