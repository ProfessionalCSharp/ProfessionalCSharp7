using System;
using System.Globalization;
using System.Windows.Data;

namespace MultiBindingSample
{
    public class PersonNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => parameter switch
            {
                "FirstLast" => values[0] + " " + values[1],
                "LastFirst" => values[1] + ", " + values[0],
                _ => throw new ArgumentException($"invalid argument {parameter}")
            };

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
