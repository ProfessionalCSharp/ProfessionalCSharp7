using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LayoutSamples.Views
{
    public sealed partial class AdaptiveRelativePanelPage : Page
    {
        public AdaptiveRelativePanelPage()
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
    }
}
