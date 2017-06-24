using System;

namespace Wrox.ProCSharp.Arrays
{
    public class Person : IComparable<Person>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";

        public int CompareTo(Person other)
        {
            if (other == null) throw new ArgumentNullException("other");

            int result = LastName.CompareTo(other.LastName);
            if (result == 0)
            {
                result = FirstName.CompareTo(other.FirstName);
            }

            return result;
        }
    }
}
