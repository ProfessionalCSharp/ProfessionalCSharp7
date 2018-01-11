using System;
using System.ComponentModel;
using System.Drawing;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace XAMLIntro
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var button2 = new Button
            {
                Content = "created dynamically"
            };
            button2.Click += async (sender, e) => await new MessageDialog("button 2 clicked").ShowAsync();
            stackPanel1.Children.Add(button2);
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await new MessageDialog("button 1 clicked").ShowAsync();
        }
    }
}
