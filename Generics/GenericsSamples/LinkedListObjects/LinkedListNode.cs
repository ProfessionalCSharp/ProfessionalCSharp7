
namespace Wrox.ProCSharp.Generics
{
    public class LinkedListNode
    {
        public LinkedListNode(object value) => Value = value;

        public object Value { get; }

        public LinkedListNode Next { get; internal set; }
        public LinkedListNode Prev { get; internal set; }
    }
}
