using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Transitions
{
    public sealed partial class PaneTransitionUserControl : UserControl
    {
        public PaneTransitionUserControl() => InitializeComponent();

        private void OnShow(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
            popup2.IsOpen = true;
            popup3.IsOpen = true;
        }

        private void OnHide(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = false;
            popup2.IsOpen = false;
            popup3.IsOpen = false;
        }
    }
}
