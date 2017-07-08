using System;
using System.Collections.Generic;

namespace DataLib
{
    public class Racer : IComparable<Racer>, IFormattable
    {

        public Racer(string firstName, string lastName, string country, int starts, int wins, IEnumerable<int> years, IEnumerable<string> cars)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Starts = starts;
            Wins = wins;
            Years = years != null ? new List<int>(years) : new List<int>();
            Cars = cars != null ? new List<string>(cars) : new List<string>();
        }
        public Racer(string firstName, string lastName, string country, int starts, int wins)
          : this(firstName, lastName, country, starts, wins, null, null) { }

        public string FirstName { get; }
        public string LastName { get; }
        public string Country { get; }
        public int Wins { get; }
        public int Starts { get; }
        public IEnumerable<string> Cars { get; }
        public IEnumerable<int> Years { get; }

        public override string ToString() => $"{FirstName} {LastName}";

        public int CompareTo(Racer other) => LastName.CompareTo(other?.LastName);

        public string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "C":
                    return Country;
                case "S":
                    return Starts.ToString();
                case "W":
                    return Wins.ToString();
                case "A":
                    return $"{FirstName} {LastName}, country: {Country}; starts: {Starts}, wins: {Wins}";
                default:
                    throw new FormatException($"Format {format} not supported");
            }
        }
    }
}