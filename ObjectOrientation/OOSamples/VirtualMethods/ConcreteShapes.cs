using System;

namespace VirtualMethods
{
    public class Rectangle : Shape
    {
        public override void Draw() =>
            Console.WriteLine($"Rectangle with {Position} and {Size}");

        public override void Move(Position newPosition)
        {
            Console.Write("Rectangle ");
            base.Move(newPosition);
        }

        public override void Resize(int width, int height)
        {
            throw new NotImplementedException();
        }
    }

    public class Ellipse : Shape
    {
        public Ellipse()
            : base() { }

        public override void Resize(int width, int height)
        {
            Size.Width = width;
            Size.Height = height;
        }
    }
}