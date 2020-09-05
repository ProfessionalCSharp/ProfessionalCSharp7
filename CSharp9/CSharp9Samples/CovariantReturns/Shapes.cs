namespace CovariantReturns
{

    public abstract class Shape
    {
        public Shape(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public Shape(Shape original)
        {
            X = original.X;
            Y = original.Y;
            Width = original.Width;
            Height = original.Height;
        }

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public abstract Shape Clone();
    }

    public class Ellipse : Shape
     {
        public Ellipse(int x, int y, int width, int height): base(x, y, width, height)
        {
        }
        public Ellipse(Ellipse original) : base(original)
        {
        }

        public override Ellipse Clone() => new Ellipse(this);
    }

    public class Rectangle : Shape
    {
        public Rectangle(int x, int y, int width, int height) : base(x, y, width, height)
        { }
    
        public Rectangle(Rectangle original) : base(original)
        {
        }

        public override Rectangle Clone() => new Rectangle(this);
    }

}
