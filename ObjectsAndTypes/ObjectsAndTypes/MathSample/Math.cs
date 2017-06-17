namespace MathSample
{
    public class Math
    {
        public int Value { get; set; }

        public int GetSquare() => Value * Value;

        public static int GetSquareOf(int x) => x * x;

        public static double GetPi() => 3.14159;
    }
}
