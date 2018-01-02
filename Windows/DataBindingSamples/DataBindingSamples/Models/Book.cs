using System.Collections.Generic;

namespace DataBindingSamples.Models
{
    public class Book : BindableBase
    {
        public Book()
        {

        }
        public Book(int id, string title, string publisher, params string[] authors)
        {
            BookId = id;
            Title = title;
            Publisher = publisher;
            Authors = authors;
        }
        public int BookId { get; }
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Publisher { get; set; }
        public IEnumerable<string> Authors { get; }

        public override string ToString() => Title;
    }
}
