using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RoutedEvents
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnTappedButton(object sender, TappedRoutedEventArgs e)
        {
            ShowStatus(nameof(OnTappedButton), e);
            e.Handled = CheckStopRouting.IsChecked == true;
        }

        private void OnTappedGrid(object sender, TappedRoutedEventArgs e)
        {
            ShowStatus(nameof(OnTappedGrid), e);
            e.Handled = CheckStopRouting.IsChecked == true;
        }

        private void ShowStatus(string status, RoutedEventArgs e)
        {
            textStatus.Text += $"{status} {e.OriginalSource.GetType().Name}";
            textStatus.Text += "\r\n";
        }

        private void OnCleanStatus(object sender, RoutedEventArgs e)
        {
            textStatus.Text = string.Empty;
        }
    }
}
