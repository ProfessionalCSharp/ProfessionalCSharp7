using System;

namespace Records
{
    public record Book(string Title, string Publisher, string? Isbn = default) 
    {
        // protected virtual Type EqualityContract => typeof(Book);

        public Book(Book book)
        {
            Title = book.Title;
            Publisher = book.Publisher;
            Isbn = book.Isbn;
        }

        public virtual Book MyClone()
        {
            return new Book(this);
        }
    }
}
