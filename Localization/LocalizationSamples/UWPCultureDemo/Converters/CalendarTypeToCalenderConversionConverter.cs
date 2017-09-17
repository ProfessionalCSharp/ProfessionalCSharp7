using System;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace UWPCultureDemo.Converters
{
    public class CalendarTypeToCalendarInformationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var c = value as Calendar;
            if (c == null) return null;
            var calText = new StringBuilder(50);
            calText.Append(c.ToString());
            calText.Remove(0, 21); // remove the namespace
            calText.Replace("Calendar", "");

            if (c is GregorianCalendar gregCal)
            {
                calText.Append($" {gregCal.CalendarType}");
            }

            return calText.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
