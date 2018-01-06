using ApplicationLifetime.Utilities;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ApplicationLifetime
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            (Application.Current as App).NavigationFrame = this.ContentFrame;
        }

        private BackButtonManager _backButtonManager;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter.ToString() == "Resume")
            {
                var suspensionManager = new NavigationSuspensionManager();
                string navigationState = await suspensionManager.GetNavigationStateAsync();
                ContentFrame.SetNavigationState(navigationState);
            }

            _backButtonManager = new BackButtonManager(Frame);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            _backButtonManager.Dispose();
        }

        public void GotoPage1() => ContentFrame.Navigate(typeof(Page1), Parameter1);

        public string Parameter1 { get; set; }

        public void GotoPage2() => ContentFrame.Navigate(typeof(Page2), Parameter2);

        public string Parameter2 { get; set; }
    }
}
