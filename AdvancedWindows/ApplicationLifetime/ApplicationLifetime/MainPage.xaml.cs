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
        }

        private BackButtonManager _backButtonManager;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _backButtonManager = new BackButtonManager(Frame);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            _backButtonManager.Dispose();
        }

        public void GotoPage1() => Frame.Navigate(typeof(Page1), ParameterPage1);

        public string ParameterPage1 { get; set; }

        public void GotoPage2() => Frame.Navigate(typeof(Page2), ParameterPage2);

        public string ParameterPage2 { get; set; }
    }
}
