namespace Wrox.ProCSharp.Composition
{
    public class FuelEconomyType
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string DistanceText { get; set; }
        public string FuelText { get; set; }

        public override string ToString() => Text;
    }
}
