using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Transitions
{
    public sealed partial class RepositionUserControl : UserControl
    {
        public RepositionUserControl() => InitializeComponent();

        private void OnReposition(object sender, RoutedEventArgs e)
        {
            buttonReposition.Margin = new Thickness(100);
            button2.Margin = new Thickness(100);
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            buttonReposition.Margin = new Thickness(10);
            button2.Margin = new Thickness(10);
        }
    }
}
