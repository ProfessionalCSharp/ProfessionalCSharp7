using System;

namespace StructsSample
{
    public struct Dimensions
    {
        public double Length { get;  }
        public double Width { get; }

        public Dimensions(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public double Diagonal => Math.Sqrt(Length * Length + Width * Width);
    }
}