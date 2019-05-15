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

namespace ControlsSamples
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

        private void OnFrame(object sender, RoutedEventArgs e)
        {
            var w = new FramesWindow();
            w.ShowDialog();
        }

        private void OnExpander(object sender, RoutedEventArgs e)
        {
            var w = new ExpanderWindow();
            w.ShowDialog();
        }

        private void OnDecorations(object sender, RoutedEventArgs e)
        {
            var w = new DecorationsWindow();
            w.ShowDialog();
        }
    }
}
