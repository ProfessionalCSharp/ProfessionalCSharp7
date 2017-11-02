namespace RelationUsingConventions
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
