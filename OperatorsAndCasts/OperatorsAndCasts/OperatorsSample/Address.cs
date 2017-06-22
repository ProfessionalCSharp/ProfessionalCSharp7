namespace OperatorsSample
{
    public class Address
    {
        public Address(string country, string city)
        {
            Country = country;
            City = city;
        }
        public string City { get; }
        public string Country { get; }
    }
}
