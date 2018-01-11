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

namespace AttachedProperty
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            MyAttachedPropertyProvider.SetMySample(button1, "sample value");

            foreach (var item in GetChildren(grid1,
                e => MyAttachedPropertyProvider.GetMySample(e) != string.Empty))
            {
                list1.Items.Add(
                  $"{item.Name}: {MyAttachedPropertyProvider.GetMySample(item)}");
            }
        }

        private IEnumerable<FrameworkElement> GetChildren(FrameworkElement element, Func<FrameworkElement, bool> pred)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child != null && pred(child))
                {
                    yield return child;
                }
            }
        }

    }
}
