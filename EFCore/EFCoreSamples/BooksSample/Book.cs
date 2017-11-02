using System.Collections.Generic;

namespace BooksSample
{
    public class Book
    {
        // parameterless constructor neeeded for EF Core
        private Book() { }

        public Book(string title, string publisher)
        {
            Title = title;
            _publisher = publisher;
        }

        private int _bookId = 0;
        public int BookId => _bookId;
        public string Title { get; set; }
        private string _publisher;
        public string Publisher => _publisher;

        public virtual List<BookAuthor> BookAuthors { get; set; }

        public override string ToString() => $"id: {_bookId}, title: {Title}, publisher: {Publisher}";
    }
}
