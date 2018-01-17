namespace HelloAndroid.Models
{
    public class SomeData
    {
        public int Number { get; set; }
        public string Text { get; set; }
        public override string ToString() => $"{Number} {Text}";
    }
}