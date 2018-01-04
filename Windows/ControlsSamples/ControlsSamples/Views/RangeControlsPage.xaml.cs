using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ControlsSamples.Views
{
    public sealed partial class RangeControlsPage : Page
    {
        public RangeControlsPage()
        {
            this.InitializeComponent();
            ShowProgress();
        }

        private void ShowProgress()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            int i = 0;
            timer.Tick += (sender, e) =>
            {
                progressBar1.Value = i++;
                if (i >= 100)
                {
                    i = 0;
                }
            };
            timer.Start();
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
