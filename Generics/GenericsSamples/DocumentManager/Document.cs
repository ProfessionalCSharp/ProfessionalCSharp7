
namespace Wrox.ProCSharp.Generics
{
    public interface IDocument
    {
        string Title { get; }
        string Content { get; }
    }

    public class Document : IDocument
    {
        public Document(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; }
        public string Content { get; }
    }
}
