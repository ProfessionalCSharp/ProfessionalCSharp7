using ApplicationLifetime.Services;
using ApplicationLifetime.Utilities;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ApplicationLifetime
{
    public sealed partial class Page1 : Page
    {
        public Page1() => InitializeComponent();

        private BackButtonManager _backButtonManager;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _backButtonManager = new BackButtonManager(Frame);

            ReceivedContent = e.Parameter?.ToString() ?? string.Empty;
            Bindings.Update();
        }

        public string ReceivedContent { get; private set; }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            _backButtonManager.Dispose();
        }

        public void GotoPage2() => Frame.Navigate(typeof(Page2), Parameter1);

        public string Parameter1 { get; set; }

        public DataManager Data => DataManager.Instance;
    }
}
