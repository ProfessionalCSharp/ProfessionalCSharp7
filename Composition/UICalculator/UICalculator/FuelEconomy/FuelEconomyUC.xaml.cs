using Windows.UI.Xaml.Controls;

namespace Wrox.ProCSharp.Composition
{
    public sealed partial class FuelEconomyUC : UserControl
    {
        public FuelEconomyUC()
        {
            this.InitializeComponent();
        }

        public FuelEconomyViewModel ViewModel { get; } = new FuelEconomyViewModel();
    }
}
