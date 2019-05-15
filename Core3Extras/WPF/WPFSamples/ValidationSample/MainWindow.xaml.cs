using System.Windows;

namespace ValidationSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SomeData _p1 = new SomeData { Value1 = 11 };


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _p1;
        }

        private void OnShowValue(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_p1.Value1.ToString());
        }

        private void OnShowNotification(object sender, RoutedEventArgs e)
        {
            new NotificationWindow().ShowDialog();
        }
    }
}
