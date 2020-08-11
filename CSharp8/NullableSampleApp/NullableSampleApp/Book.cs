namespace NullableSampleApp
{
    public class Book
    {
        public Book(string title)
        {
            Title = title;
        }

        public int BookId { get; set; }
        public string Title { get; set; }
        public string? Publisher { get; set; }
    }
}
