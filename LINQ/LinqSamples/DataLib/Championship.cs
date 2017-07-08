namespace DataLib
{
    public class Championship
    {
        public Championship(int year, string first, string second, string third)
        {
            Year = year;
            First = first;
            Second = second;
            Third = third;
        }
        public int Year { get; }
        public string First { get; }
        public string Second { get; }
        public string Third { get; }
    }
}