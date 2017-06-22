using System;

namespace CustomIndexerSample
{
    public class Person
    {
        public DateTime Birthday { get; }
        public string FirstName { get;  }
        public string LastName { get;  }

        public Person(string firstName, string lastName, DateTime birthDay)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthDay;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}