using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LayoutSamples.Views
{
    public sealed partial class DelayLoadingPage : Page
    {
        public DelayLoadingPage()
        {
            this.InitializeComponent();
        }

        private void OnDeferLoad(object sender, RoutedEventArgs e)
        {
            FindName(nameof(deferGrid));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
              Frame.CanGoBack ? AppViewBackButtonVisibility.Visible :
              AppViewBackButtonVisibility.Collapsed;

            base.OnNavigatedTo(e);
        }
    }
}
