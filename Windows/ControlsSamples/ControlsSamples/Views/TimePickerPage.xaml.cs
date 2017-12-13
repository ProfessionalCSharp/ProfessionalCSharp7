using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ControlsSamples.Views
{
    public sealed partial class TimePickerPage : Page
    {
        public TimePickerPage()
        {
            this.InitializeComponent();
        }

        private DateTimeOffset _date = DateTimeOffset.UtcNow.AddDays(100).AddHours(3);
        public DateTimeOffset Date
        {
            get => _date;
            set => _date = value;
        }

        private TimeSpan _timeSpan = new TimeSpan(10, 10, 30);
        public TimeSpan Time
        {
            get => _timeSpan;
            set => _timeSpan = value;
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e) =>
            await new MessageDialog($"Date: {Date} {Time}").ShowAsync();

        private void OnOpenDatePicker()
        {

        }
    }
}
