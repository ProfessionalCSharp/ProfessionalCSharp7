using Windows.UI.Xaml.Controls;
using Wrox.ProCSharp.Composition;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPCalculatorHost
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ViewModel = new CalculatorExtensionsViewModel();
            ViewModel.Init(typeof(FuelCalculatorExtension), typeof(TemperatureConversionExtension));
            calculatorExtensionsUC.ViewModel = ViewModel;
        }

        public CalculatorExtensionsViewModel ViewModel { get; }
    }
}
