using Windows.UI.Xaml.Markup;

namespace CustomMarkupExtension
{
    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public class CalculatorExtension : MarkupExtension
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Operation Operation { get; set; }
        protected override object ProvideValue()
        {
            double result = 0;
            switch (Operation)
            {
                case Operation.Add:
                    result = X + Y;
                    break;
                case Operation.Subtract:
                    result = X - Y;
                    break;
                case Operation.Multiply:
                    result = X * Y;
                    break;
                case Operation.Divide:
                    result = X / Y;
                    break;
                default:
                    break;
            }
            return result.ToString();
        }
    }
}
