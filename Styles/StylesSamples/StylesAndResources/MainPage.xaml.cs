using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StylesAndResources
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

        private void OnOpenResources(object sender, RoutedEventArgs e)
        {
            var page = new ResourceDemoPage();
            this.Frame.Navigate(typeof(ResourceDemoPage));

        }

        private void OnOpenThemePage(object sender, RoutedEventArgs e)
        {
            var page = new ResourceDemoPage();
            this.Frame.Navigate(typeof(ThemeDemoPage));
        }
    }
}
