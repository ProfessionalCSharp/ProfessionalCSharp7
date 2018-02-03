using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Transitions
{
    public sealed partial class ListItemsUserControl : UserControl
    {
        public ListItemsUserControl() => InitializeComponent();


        private void OnAdd(object sender, RoutedEventArgs e)
        {
            list1.Items.Add(CreateRectangle());
            list2.Items.Add(CreateRectangle());
        }

        private Rectangle CreateRectangle() =>
            new Rectangle
            {
                Width = 90,
                Height = 40,
                Margin = new Thickness(5),
                Fill = new SolidColorBrush { Color = Colors.Blue }
            };

        private void OnRemove(object sender, RoutedEventArgs e)
        {
            if (list1.Items.Count > 0)
            {
                list1.Items.RemoveAt(0);
                list2.Items.RemoveAt(0);
            }
        }
    }
}
