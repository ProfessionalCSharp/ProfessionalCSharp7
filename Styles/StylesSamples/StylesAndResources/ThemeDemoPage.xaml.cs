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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StylesAndResources
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThemeDemoPage : Page
    {
        public ThemeDemoPage()
        {
            this.InitializeComponent();
        }

        private void OnChangeTheme(object sender, RoutedEventArgs e)
        {
            grid1.RequestedTheme = grid1.RequestedTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;

        }
    }
}
