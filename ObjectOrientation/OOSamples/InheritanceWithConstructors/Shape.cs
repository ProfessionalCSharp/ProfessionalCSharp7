using System;

namespace InheritanceWithConstructors
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString() => $"X: {X}, Y: {Y}";
    }

    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public override string ToString() => $"Width: {Width}, Height: {Height}";
    }

    public class Shape
    {
        public Shape(int width, int height, int x, int y)
        {
            Size = new Size { Width = width, Height = height };
            Position = new Position { X = x, Y = y };
        }

        public Position Position { get; }
        public Size Size { get; }

        public virtual void Draw() => Console.WriteLine($"Shape with {Position} and {Size}");

        public virtual void Move(Position newPosition)
        {
            Position.X = newPosition.X;
            Position.Y = newPosition.Y;
            Console.WriteLine($"moves to {Position}");
        }
    }
}