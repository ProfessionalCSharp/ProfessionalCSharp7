using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Wrox.ProCSharp.Composition;

namespace UWPCalculatorHost.Views
{
    public sealed partial class CalculatorUI : UserControl
    {
        public CalculatorUI()
        {
            this.InitializeComponent();
            ViewModel = new CalculatorViewModel();
            ViewModel.Init(typeof(Calculator), typeof(SubtractOperation), typeof(AddOperation), typeof(SlowAddOperation));
        }

        public CalculatorViewModel ViewModel { get; }

        public void OnNumberClick(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                ViewModel.Input += b.Content.ToString();
            }
        }

        public void DefineOperation(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                ViewModel.CurrentOperation = b.Tag as IOperation;
            }
        }
    }
}
