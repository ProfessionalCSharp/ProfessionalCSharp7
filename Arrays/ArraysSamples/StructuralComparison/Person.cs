using System;

namespace Wrox.ProCSharp.Arrays
{
    public class Person : IEquatable<Person>
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Person(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{Id} {FirstName} {LastName}";


        public override bool Equals(object obj)
        {
            if (obj == null)
                return base.Equals(obj);
            return Equals(obj as Person);
        }

        public override int GetHashCode() => Id.GetHashCode();

        #region IEquatable<Person> Members

        public bool Equals(Person other)
        {
            if (other == null)
                return base.Equals(other);

            return Id == other.Id && FirstName == other.FirstName && LastName == other.LastName;
        }

        #endregion
    }

}
