using System.Collections.Generic;

namespace DataBindingSamples.Models
{
    public class Book : BindableBase
    {
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
            set => Set(ref _title, value);
        }

        private string _publisher;
        public string Publisher
        {
            get => _publisher;
            set => Set(ref _publisher, value);
        }
        public IEnumerable<string> Authors { get; }

        public override string ToString() => Title;
    }
}
