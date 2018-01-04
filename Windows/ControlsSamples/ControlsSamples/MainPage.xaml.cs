using ControlsSamples.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ControlsSamples
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnText(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(TextPage));

        private void OnPresenters() => Frame.Navigate(typeof(PresentersPage));

        private void OnRange() => Frame.Navigate(typeof(RangeControlsPage));

        private void OnButtons() => Frame.Navigate(typeof(ButtonsPage));
    }
}
