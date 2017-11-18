using System;

namespace BookServiceClientApp.Models
{
    public class BookChapter
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
    }

}
