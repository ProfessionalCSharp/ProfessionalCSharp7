namespace TriggerSamples
{
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

        public override string ToString() => Title;
    }

}
