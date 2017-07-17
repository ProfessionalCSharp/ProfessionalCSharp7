namespace UsingStatic
{
    public class Person
    {
        public Person(string name) => name.Split(' ').ToStrings(out _firstName, out _lastName);

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
