using ShareTarget.ViewModels;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShareTarget
{
    public sealed partial class ShareTargetPage : Page
    {
        public ShareTargetPage() => this.InitializeComponent();

        public ShareTargetPageViewModel ViewModel { get; } = new ShareTargetPageViewModel();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Activate(e.Parameter as ShareOperation);

            base.OnNavigatedTo(e);
        }
    }
}
