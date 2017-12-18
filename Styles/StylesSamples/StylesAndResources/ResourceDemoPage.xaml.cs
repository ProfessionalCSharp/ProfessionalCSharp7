using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace StylesAndResources
{
    public sealed partial class ResourceDemoPage : Page
    {
        public ResourceDemoPage()
        {
            this.InitializeComponent();
        }

        private void OnApplyResources(object sender, RoutedEventArgs e)
        {
            if (sender is Control ctrl)
            {
                ctrl.Background = ctrl.TryFindResource("MyGradientBrush") as Brush;
            }
        }
    }
}
