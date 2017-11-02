using System.Collections.Generic;

namespace BooksSample
{
    public class Author
    {
        private Author() { }

        public Author(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        private int _authorId = 0;
        public int AuthorId => _authorId;

        private string _firstName;
        public string FirstName => _firstName;
        private string _lastName;
        public string LastName => _lastName;

        public virtual List<BookAuthor> BookAuthors { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
