using System.Runtime.CompilerServices;

namespace NullabilitySample
{
#nullable disable
    class LegacyBook
    {
        public string Title { get; }
        public string Publisher { get; }
        public LegacyBook(string title, string publisher)
        {
            Title = title;
            Publisher = publisher;
        }

        public LegacyBook(string title)
            : this(title, null) { }

        public void Deconstruct(out string title, out string publisher)
            => (title, publisher) = (Title, Publisher);

        public override string ToString() => Title;
    }
}
