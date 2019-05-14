using System.Windows;
using System.Windows.Input;

namespace RoutedEventsWPF
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

        private bool IsButton1Source(RoutedEventArgs e) => 
            (e.Source as FrameworkElement).Name == nameof(button1);


        private void OnButtonMouseMove(object sender, MouseEventArgs e)
        {
            ShowStatus(nameof(OnButtonMouseMove), e);
            e.Handled = CheckStopBubbling.IsChecked == true;
        }

        private void OnGridMouseMove(object sender, MouseEventArgs e)
        {
            if (CheckIgnoreGridMove.IsChecked == true && !IsButton1Source(e)) return;

            ShowStatus(nameof(OnGridMouseMove), e);
            e.Handled = CheckStopBubbling.IsChecked == true;
        }

        private void OnGridPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (CheckIgnoreGridMove.IsChecked == true && !IsButton1Source(e)) return;

            ShowStatus(nameof(OnGridPreviewMouseMove), e);
            e.Handled = CheckStopPreview.IsChecked == true;
        }

        private void OnButtonPreviewMouseMove(object sender, MouseEventArgs e)
        {
            ShowStatus(nameof(OnButtonPreviewMouseMove), e);
            e.Handled = CheckStopPreview.IsChecked == true;
        }

        private void ShowStatus(string status, RoutedEventArgs e)
        {
            textStatus.Text += $"{status} source: {e.Source.GetType().Name} {(e.Source as FrameworkElement)?.Name}, original source: {e.OriginalSource.GetType().Name}";
            textStatus.Text += "\r\n";
        }

        private void OnCleanStatus(object sender, RoutedEventArgs e)
        {
            textStatus.Text = string.Empty;
        }
    }
}
