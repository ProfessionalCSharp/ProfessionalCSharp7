using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VisualStates
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

        private void OnEnable(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Enabled", useTransitions: true);
        }

        private void OnDisable(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Disabled", useTransitions: true);
        }
    }
}
