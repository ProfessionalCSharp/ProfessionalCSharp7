using System.Windows;

namespace TriggerSamples
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

        private void OnPropertyTrigger(object sender, RoutedEventArgs e)
        {
            var w = new PropertyTriggerWindow();
            w.ShowDialog();
        }

        private void OnMultiTrigger(object sender, RoutedEventArgs e)
        {
            var w = new MultiTriggerWindow();
            w.ShowDialog();
        }

        private void OnDataTrigger(object sender, RoutedEventArgs e)
        {
            var w = new DataTriggerWindow();
            w.ShowDialog();
        }
    }
}
