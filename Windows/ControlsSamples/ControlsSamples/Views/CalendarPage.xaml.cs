using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ControlsSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            this.InitializeComponent();
            MinDate = new DateTimeOffset(new DateTime(1950, 1, 1));
        }

        public IEnumerable<string> DisplayModes => Enum.GetNames(typeof(CalendarViewDisplayMode));

        public string CurrentDisplayMode
        {
            get => (string)GetValue(CurrentDisplayModeProperty);
            set => SetValue(CurrentDisplayModeProperty, value);
        }

        public static readonly DependencyProperty CurrentDisplayModeProperty =
            DependencyProperty.Register("CurrentDisplayMode", typeof(string), typeof(CalendarPage), new PropertyMetadata(CalendarViewDisplayMode.Month.ToString()));

        public DateTimeOffset MinDate { get; set; }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void OnDateSelected(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            await new MessageDialog($"selected date: {sender.SelectedDates.First():D}").ShowAsync();
        }
    }
}
