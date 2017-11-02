namespace OwnedEntities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public Address PrivateAddress { get; set; }
        public Address CompanyAddress { get; set; }
    }
}
