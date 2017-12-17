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
    public sealed partial class ResourceDemoPage : Page
    {
        public ResourceDemoPage()
        {
            this.InitializeComponent();
        }

        private void OnApplyResources(object sender, RoutedEventArgs e)
        {
            Control ctrl = sender as Control;
            ctrl.Background = ctrl.TryFindResource("MyGradientBrush") as Brush;

        }
    }
}
