namespace InheritanceWithConstructors
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Rectangle();
            r.Position.X = 33;
            r.Position.Y = 22;
            r.Size.Width = 200;
            r.Size.Height = 100;
            r.Draw();
            DrawShape(r);

            r.Move(new Position { X = 120, Y = 40 });
            r.Draw();

            Shape s1 = new Ellipse();
            DrawShape(s1);
        }

        public static void DrawShape(Shape shape) => shape.Draw();
    }
}
