using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static WinAppAnalytics.EventNames;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinAppAnalytics
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
            base.OnNavigatedTo(e);
            Analytics.TrackEvent(PageNavigation, new Dictionary<string, string> { ["Page"] = nameof(MainPage) });
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent(ButtonClicked, new Dictionary<string, string> { ["State"] = textState.Text });
        }

        private async void OnAnalyticsChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkbox)
            {
                bool isChecked = checkbox?.IsChecked ?? true;
                await Analytics.SetEnabledAsync(isChecked);
            }
        }
    }
}
