using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using System;

namespace AttachedPropertyDemoWPF
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyAttachedPropertyProvider.SetMySample(button1, "sample value");
            //foreach (var item in GetChildren(grid1, e => MyAttachedPropertyProvider.GetMySample(e) != string.Empty))
            //{
            //    list1.Items.Add($"{item.Name}: {MyAttachedPropertyProvider.GetMySample(item)}");
            //}
            foreach (var item in LogicalTreeHelper.GetChildren(grid1).OfType<FrameworkElement>().Where(e => MyAttachedPropertyProvider.GetMySample(e) != string.Empty))
            {
                list1.Items.Add($"{item.Name}: {MyAttachedPropertyProvider.GetMySample(item)}");
            }
        }

        //private IEnumerable<FrameworkElement> GetChildren(FrameworkElement element, Func<FrameworkElement, bool> pred)
        //{
        //    int childrenCount = VisualTreeHelper.GetChildrenCount(element);
        //    for (int i = 0; i < childrenCount; i++)
        //    {
        //        FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
        //        if (child != null && pred(child))
        //        {
        //            yield return child;
        //        }
        //    }
        //}


    }
}
