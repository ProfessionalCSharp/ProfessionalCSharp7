using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsAppChatClient.ViewModels;

namespace WindowsAppChatClient
{
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

        public ChatViewModel ViewModel { get; } = ApplicationServices.Instance.ServiceProvider.GetService<ChatViewModel>();

        private void OnGotoGroupChat(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GroupChatPage));
        }
    }
}
