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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DateSelectionSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OnDateChanged1(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            await new MessageDialog($"date changed to {args.AddedDates.First().Date}").ShowAsync();
        }

        private async void OnDateChanged2(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            await new MessageDialog($"date changed to {args.NewDate}").ShowAsync();
        }

        private async void OnDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            await new MessageDialog($"date changed to {args.NewDate}").ShowAsync();
        }
    }
}
