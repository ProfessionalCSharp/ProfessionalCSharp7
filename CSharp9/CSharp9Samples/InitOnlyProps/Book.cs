namespace InitOnlyProps
{
    public class Book
    {
        public string Title { get; init; } = string.Empty;
        public string? Publisher { get; init; }

        private readonly string? _isbn;

        public string? Isbn
        {
            get => _isbn;
            init => _isbn = value;
        }
    }
}
