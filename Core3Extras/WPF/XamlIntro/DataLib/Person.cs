namespace DataLib
{
    public class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public override string ToString() => $"{FirstName} {LastName}";
    }
}
