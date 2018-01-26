using System.Composition;

namespace Wrox.ProCSharp.Composition
{
    [Export(typeof(ICalculatorExtension))]
    [CalculatorExtensionMetadata(
      Title = "Temperature",
      Description = "Temperature conversion",
      ImageUri = "ms-appx:///TemperaturConversion/Images/Temperature.png")]
    public class TemperatureConversionExtension : ICalculatorExtension
    {
        private object _control;
        public object UI => _control ?? (_control = new TemperatureConversionUC());
    }
}
