using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ControlsSamples.Converters
{
    public class CalendarDisplayModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Enum.TryParse<CalendarViewDisplayMode>(value.ToString(), out CalendarViewDisplayMode displayMode))
            {
                return displayMode;
            }
            else
            {
                return CalendarViewDisplayMode.Month;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Enum.GetName(typeof(CalendarViewDisplayMode), value);
        }
    }
}
