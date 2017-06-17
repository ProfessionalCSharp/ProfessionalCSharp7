using static System.Console;

namespace VirtualMethods
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

    public abstract class Shape
    {
        public Position Position { get; } = new Position();
        public Size Size { get; } = new Size();

        public virtual void Draw() => WriteLine($"Shape with {Position} and {Size}");

        public virtual void Move(Position newPosition)
        {
            Position.X = newPosition.X;
            Position.Y = newPosition.Y;
            WriteLine($"moves to {Position}");
        }

        public abstract void Resize(int width, int height);
    }

}