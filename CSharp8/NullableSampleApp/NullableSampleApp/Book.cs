namespace NullableSampleApp
{
    public class Book
    {
        public string Isbn { get; }
        public string Title { get; }
        public string? Publisher { get; }
        public Book(string isbn, string title, string? publisher = default) =>
            (Isbn, Title, Publisher) = (isbn, title, publisher);

        public void Deconstruct(out string isbn, out string title,
                                out string? publisher) =>
            (isbn, title, publisher) = (Isbn, Title, Publisher);

        public override string ToString() => Title;
    }

}
