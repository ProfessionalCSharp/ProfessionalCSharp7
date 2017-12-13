using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PageNavigation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondPage : NavigationPage
    {
        public SecondPage()
        {
            this.InitializeComponent();
        }

        public string Data { get; set; }

        public void OnNavigateToThirdPage()
        {
            Frame.Navigate(typeof(ThirdPage), Data);
        }
    }
}
