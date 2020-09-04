using System;

namespace Records
{
    record Position(int X, int Y) 
    {
        public override string ToString() => $"x: {X}, y: {Y}"; 
    }

    abstract record Shape(Position Position) 
    {
        public override string ToString() => $"pos: {Position}";
    }

    record Rectangle(Position Position, int Width, int Height) : Shape(Position)
    {
        public override string ToString() => $"{this.GetType().Name}; {base.ToString()}; width: {Width}; height: {Height}";
    }
}
