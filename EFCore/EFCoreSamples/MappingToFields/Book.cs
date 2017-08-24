namespace MappingToFields
{
    public class Book
    {
        public Book()
        {

        }
        private int _bookId;
        public string Title { get; }
        public string Publisher { get; }

        public override string ToString() => $"id: {_bookId}, title: {Title}, publisher: {Publisher}";
    }
}
