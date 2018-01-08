namespace AutoSuggestSample.Models
{
    public class Racer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public override string ToString() => $"{FirstName} {LastName}, {Country}";
    }
}
