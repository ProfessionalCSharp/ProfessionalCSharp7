using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Wrox.ProCSharp.Composition;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWPCalculatorHost.Views
{
    public sealed partial class CalculatorExtensionsUC : UserControl
    {
        public CalculatorExtensionsUC()
        {
            this.InitializeComponent();
        }

        public CalculatorExtensionsViewModel ViewModel
        {
            get { return (CalculatorExtensionsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(CalculatorExtensionsViewModel), typeof(CalculatorExtensionsUC), new PropertyMetadata(null));
    }
}
