namespace PipelineSample
{
    public class Info
    {
        public Info(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }
        public string Color { get; set; }

        public override string ToString() => $"{Count} times: {Word}";
    }
}