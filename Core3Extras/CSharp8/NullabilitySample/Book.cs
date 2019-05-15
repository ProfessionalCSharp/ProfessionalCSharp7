namespace NullabilitySample
{
    public class Book
    {
        public Book(string title, string? publisher)
            => (Title, Publisher) = (title, publisher);

        public Book(string title)
            : this(title, null) { }

        public string Title { get; }
        public string? Publisher { get; }


        public void Deconstruct(out string title, out string? publisher)
            => (title, publisher) = (Title, Publisher);

        public override string ToString() => Title;
    }
}
