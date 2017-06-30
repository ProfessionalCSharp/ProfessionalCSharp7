using System;


namespace Wrox.ProCSharp.Collections
{
    public class EmployeeIdException : Exception
    {
        public EmployeeIdException(string message) : base(message) { }
    }

    public struct EmployeeId : IEquatable<EmployeeId>
    {
        private readonly char _prefix;
        private readonly int _number;

        public EmployeeId(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            _prefix = (id.ToUpper())[0];
            int numLength = id.Length - 1;
            try
            {
                _number = int.Parse(id.Substring(1, numLength > 6 ? 6 : numLength));
            }
            catch (FormatException)
            {
                throw new EmployeeIdException("Invalid EmployeeId format");
            }
        }

        public override string ToString() => _prefix.ToString() + $"{_number,6:000000}";

        public override int GetHashCode() => (_number ^ _number << 16) * 0x1505_1505;

        public bool Equals(EmployeeId other) => _prefix == other._prefix && _number == other._number;

        public override bool Equals(object obj) => Equals((EmployeeId)obj);

        public static bool operator ==(EmployeeId left, EmployeeId right) => left.Equals(right);

        public static bool operator !=(EmployeeId left, EmployeeId right) => !(left == right);
    }
}
