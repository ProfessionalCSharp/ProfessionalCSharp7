using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HelloWindows
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("Hello, Windows!").ShowAsync();
        }
    }
}
