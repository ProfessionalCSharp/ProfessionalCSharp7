using MapSample.ViewModels;
using Windows.UI.Xaml.Controls;

namespace MapSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MapsViewModel(Map);
        }

        public MapsViewModel ViewModel { get; }
    }
}
