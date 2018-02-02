using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using WindowsAppChatClient.ViewModels;

namespace WindowsAppChatClient
{
    public sealed partial class GroupChatPage : Page
    {
        public GroupChatPage() => InitializeComponent();

        public GroupChatViewModel ViewModel { get; } = ApplicationServices.Instance.ServiceProvider.GetService<GroupChatViewModel>();
    }
}
