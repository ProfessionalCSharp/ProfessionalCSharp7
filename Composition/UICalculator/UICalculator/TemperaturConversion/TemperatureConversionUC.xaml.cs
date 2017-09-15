using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Wrox.ProCSharp.Composition
{
    public sealed partial class TemperatureConversionUC : UserControl
    {
        public TemperatureConversionUC()
        {
            this.InitializeComponent();
        }

        public TemperatureConversionViewModel ViewModel { get; } = new TemperatureConversionViewModel();
    }
}
