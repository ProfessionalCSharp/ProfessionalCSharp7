using LayoutSamples.Views;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LayoutSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        private void OnResizePage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ResizePage));
        private void OnReflowPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(ReflowPage));
        private void OnStackPanelPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(StackPanelPage));
        private void OnCanvasPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(CanvasPage));
        private void OnGridPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(GridPage));
        private void OnVariableSizedWrapGrid(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(VariableSizedWrapGridPage));
        private void OnRelativePanelPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(RelativePanelPage));
        private void OnAdaptiveRelativePanelPage(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(AdaptiveRelativePanelPage));
        private void OnDelayLoading(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(DelayLoadingPage));
    }
}
