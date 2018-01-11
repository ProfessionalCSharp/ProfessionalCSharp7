using LayoutSamples.Views;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LayoutSamples
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
              Frame.CanGoBack ? AppViewBackButtonVisibility.Visible :
              AppViewBackButtonVisibility.Collapsed;

            base.OnNavigatedTo(e);
        }

        private void OnStackPanelPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(StackPanelPage));
        private void OnCanvasPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(CanvasPage));
        private void OnGridPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(GridPage));
        private void OnVariableSizedWrapGrid(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(VariableSizedWrapGridPage));
        private void OnRelativePanelPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(RelativePanelPage));
        private void OnAdaptiveRelativePanelPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(AdaptiveRelativePanelPage));
        private void OnDelayLoading(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(DelayLoadingPage));
    }
}
