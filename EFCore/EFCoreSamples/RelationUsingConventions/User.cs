using System.Collections.Generic;

namespace RelationUsingConventions
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Book> AuthoredBooks { get; set; }
    }
}
