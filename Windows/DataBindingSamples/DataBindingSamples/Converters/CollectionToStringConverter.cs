using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace DataBindingSamples.Converters
{
    public class CollectionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            IEnumerable<string> names = (IEnumerable<string>)value;
            return string.Join(parameter?.ToString() ?? ", ", names);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
