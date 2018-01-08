using ApplicationLifetime.Utilities;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ApplicationLifetime
{
    public sealed partial class Page2 : Page
    {
        public Page2() => InitializeComponent();

        private BackButtonManager _backButtonManager;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _backButtonManager = new BackButtonManager(Frame);

            ReceivedContent = e.Parameter?.ToString() ?? string.Empty;
            Bindings.Update();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
           _backButtonManager.Dispose();
        }

        public string ReceivedContent { get; private set; }
    }
}
