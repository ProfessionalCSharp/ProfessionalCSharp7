namespace Models
{
    public sealed class Country : ICountry
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public override string ToString() => Name;
    }
}
