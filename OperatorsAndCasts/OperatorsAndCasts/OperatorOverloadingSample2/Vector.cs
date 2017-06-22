using System;

namespace OperatorOverloadingSample
{
    public struct Vector
    {

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

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public override string ToString() => $"( {X}, {Y}, {Z} )";

        public static Vector operator +(Vector left, Vector right) =>
            new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector operator *(double left, Vector right) =>
          new Vector(left * right.X, left * right.Y, left * right.Z);

        public static Vector operator *(Vector left, double right) =>
          right * left;

        public static double operator *(Vector left, Vector right) =>
          left.X * right.X + left.Y * right.Y + left.Z * right.Z;

    }

}