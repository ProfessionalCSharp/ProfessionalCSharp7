using System;
using System.Collections;
using System.Collections.Generic;
using WhatsNewAttributes;

[assembly: SupportsWhatsNew]

namespace VectorClass
{
    [LastModified("19 Jul 2017", "updated for C# 7 and .NET Core 2")]
    [LastModified("6 Jun 2015", "updated for C# 6 and .NET Core")]
    [LastModified("14 Dec 2010", "IEnumerable interface implemented: " +
        "Vector can be treated as a collection")]
    [LastModified("10 Feb 2010", "IFormattable interface implemented " +
        "Vector accepts N and VE format specifiers")]
    public class Vector : IFormattable, IEnumerable<double>
    {
        public Vector(double x, double y, double z)
        {
            X = x;

            Y = y;
            Z = z;
        }

        [LastModified("19 Jul 2017", "Reduced the number of code lines")]
        public Vector(Vector vector)
            : this (vector.X, vector.Y, vector.Z) { }

        public double X { get;  }
        public double Y { get; }
        public double Z { get; }

        public override bool Equals(object obj) => this == obj as Vector;

        public override int GetHashCode() =>  (int)X | (int)Y | (int)Z;

        [LastModified("19 Jul 2017",
              "changed ijk format from StringBuilder to format string")]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return ToString();
            }

            switch (format.ToUpper())
            {
                case "N":
                    return "|| " + Norm().ToString() + " ||";
                case "VE":
                    return $"( {X:E}, {Y:E}, {Z:E} )";
                case "IJK":
                    return $"{X} i + {Y} j + {Z} k";
                default:
                    return ToString();
            }
        }

        [LastModified("6 Jun 2015", "added to implement IEnumerable<T>")]
        public IEnumerator<double> GetEnumerator() => new VectorEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => $"({X} , {Y}, {Z}";

        public double this[uint i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        throw new IndexOutOfRangeException(
                            "Attempt to retrieve Vector element" + i);
                }
            }
        }

        public static bool operator == (Vector left, Vector right) =>
            Math.Abs(left.X - right.X) < double.Epsilon &&
            Math.Abs(left.Y - right.Y) < double.Epsilon &&
            Math.Abs(left.Z - right.Z) < double.Epsilon;
    

        public static bool operator != (Vector left, Vector right) => !(left == right);

        public static Vector operator + (Vector left, Vector right) =>  new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector operator * (double left, Vector right) =>
            new Vector(left * right.X, left * right.Y, left * right.Z);

        public static Vector operator * (Vector left, double right) => left * right;

        public static double operator * (Vector left, Vector right) =>
            left.X * right.X + left.Y + right.Y + left.Z * right.Z;

        public double Norm() => X * X + Y * Y + Z * Z;

        #region enumerator class
        [LastModified("6 Jun 2015", "Change to implement the generic version IEnumerator<T>")]
        [LastModified("14 Feb 2010", "Class created as part of collection support for Vector")]
        private class VectorEnumerator : IEnumerator<double>
        {
            readonly Vector _theVector;      // Vector object that this enumerato refers to 
            int _location;   // which element of _theVector the enumerator is currently referring to 

            public VectorEnumerator(Vector theVector)
            {
                _theVector = theVector;
                _location = -1;
            }

            public bool MoveNext()
            {
                ++_location;
                return (_location <= 2);
            }

            public object Current => Current;

            double IEnumerator<double>.Current
            {
                get
                {
                    if (_location < 0 || _location > 2)
                        throw new InvalidOperationException(
                            "The enumerator is either before the first element or " +
                            "after the last element of the Vector");
                    return _theVector[(uint)_location];
                }
            }

            public void Reset()
            {
                _location = -1;
            }

            public void Dispose()
            {
                // nothing to cleanup
            }
        }
        #endregion
    }
}

