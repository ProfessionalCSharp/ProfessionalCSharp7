using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LayoutSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnStackPanel(object sender, RoutedEventArgs e)
        {
            var w = new StackPanelWindow();
            w.ShowDialog();
        }

        private void OnWrapPanel(object sender, RoutedEventArgs e)
        {
            var w = new WrapPanelWindow();
            w.ShowDialog();
        }

        private void OnCanvas(object sender, RoutedEventArgs e)
        {
            var w = new CanvasWindow();
            w.ShowDialog();
        }

        private void OnDockPanel(object sender, RoutedEventArgs e)
        {
            var w = new DockPanelWindow();
            w.ShowDialog();
        }

        private void OnGrid(object sender, RoutedEventArgs e)
        {
            var w = new GridWindow();
            w.ShowDialog();
        }
    }
}
