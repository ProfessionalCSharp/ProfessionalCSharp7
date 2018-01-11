using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

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

        public DateTimeOffset MinDate { get; } = DateTimeOffset.Parse("1/1/1965", new CultureInfo("en-US"));

        private void OnDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            switch (args.Phase)
            {
                case 0:
                    RegisterUpdateCallback();
                    break;
                case 1:
                    SetBlackoutDates();
                    break;
                case 2:
                    SetBookings();
                    break;
                default:
                    break;
            }

            void RegisterUpdateCallback() => args.RegisterUpdateCallback(OnDayItemChanging);

            void SetBlackoutDates()
            {
                if (args.Item.Date < DateTimeOffset.Now || args.Item.Date.DayOfWeek == DayOfWeek.Saturday || args.Item.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    args.Item.IsBlackout = true;
                }
                RegisterUpdateCallback();
            }

            void SetBookings()
            {
                var bookings = GetBookings().ToList();

                var booking = bookings.SingleOrDefault(b => b.day.Date == args.Item.Date.Date);
                if (booking.bookings > 0)
                {
                    var colors = new List<Color>();
                    for (int i = 0; i < booking.bookings; i++)
                    {
                        if (args.Item.Date.DayOfWeek == DayOfWeek.Saturday || args.Item.Date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            colors.Add(Colors.Red);
                        }
                        else
                        {
                            colors.Add(Colors.Green);
                        }
                    }

                    args.Item.SetDensityColors(colors);
                }
                RegisterUpdateCallback();
            }
        }

        private IEnumerable<(DateTimeOffset day, int bookings)> GetBookings()
        {
            int[] bookingDays = { 2, 3, 5, 8, 12, 13, 18, 21, 23, 27 };
            int[] bookingsPerDay = { 1, 4, 3, 6, 4, 5, 1, 3, 1, 1 };

            for (int i = 0; i < 10; i++)
            {
                yield return (DateTimeOffset.Now.Date.AddDays(bookingDays[i]), bookingsPerDay[i]);
            }
        }

        private List<DateTimeOffset> currentDatesSelected = new List<DateTimeOffset>();

        private async void OnDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {

            currentDatesSelected.AddRange(args.AddedDates);
            args.RemovedDates.ToList().ForEach(date => currentDatesSelected.Remove(date));

            string selectedDates = string.Join(", ", currentDatesSelected.Select(d => d.ToString("d")));

            await new MessageDialog($"dates selected: {selectedDates}").ShowAsync();
        }

        private async void OnDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            await new MessageDialog($"date changed to {args.NewDate}").ShowAsync();
        }

        private async void OnDateChanged1(object sender, DatePickerValueChangedEventArgs e)
        {
            await new MessageDialog($"date changed to {e.NewDate}").ShowAsync();
        }

        private async void OnDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            await new MessageDialog($"date changed to {args.NewDate}").ShowAsync();
        }
    }
}
