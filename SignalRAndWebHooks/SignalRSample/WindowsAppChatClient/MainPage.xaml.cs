using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsAppChatClient.ViewModels;

namespace WindowsAppChatClient
{
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

        public ChatViewModel ViewModel { get; } = (Application.Current as App).AppServices.GetService<ChatViewModel>();

        private void OnGotoGroupChat(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GroupChatPage));
        }
    }
}
