using System.Windows;

namespace ValidationSample
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private SomeDataWithNotifications _data = new SomeDataWithNotifications();
        public NotificationWindow()
        {
            InitializeComponent();
            this.DataContext = _data;
        }
    }
}
