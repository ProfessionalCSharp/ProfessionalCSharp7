using System;

namespace OverloadingComparisonSample
{
    public struct Vector : IEquatable<Vector>
    {
        private readonly double X, Y, Z;

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public override string ToString() => $"( {X}, {Y}, {Z} )";

        public static bool operator ==(Vector left, Vector right)
        {
            if (ReferenceEquals(left, right)) return true;

            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }


        public static bool operator !=(Vector lhs, Vector rhs) =>
           !(lhs == rhs);

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return this == (Vector)obj;
        }

        public override int GetHashCode() =>
            X.GetHashCode() + (Y.GetHashCode() << 4) + (Z.GetHashCode() << 8);

        public bool Equals(Vector other) => this == other;


        public static Vector operator +(Vector left, Vector right) =>
            new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector operator *(double left, Vector right) =>
            new Vector(left * right.X, left * right.Y, left * right.Z);

        public static Vector operator *(Vector lhs, double rhs) =>
            rhs * lhs;

        public static double operator *(Vector left, Vector right) =>
            left.X * right.X + left.Y * right.Y + left.Z * right.Z;
    }
}