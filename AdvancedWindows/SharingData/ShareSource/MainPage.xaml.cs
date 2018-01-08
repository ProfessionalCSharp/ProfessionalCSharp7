using ShareSource.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ShareSource
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public ShareDataViewModel DataSharing { get; set; } = new ShareDataViewModel();
    }
}
