namespace OperatorsSample
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Address WorkAddress { get; }
        public int? Age { get; }
        public Person(string firstName, string lastName)
            : this(firstName, lastName, 0) { }

        public Person(string firstName, string lastName, int age)
            : this(firstName, lastName, age, null) { }

        public Person(string firstName, string lastName, int age, Address workAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            WorkAddress = workAddress;
        }
    }
}
