using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UWPSwitchExpressionSample.Utilities
{
    public class TrafficLightToBrushConverter : IValueConverter
    {
        private readonly Brush _redBrush = new SolidColorBrush(Colors.Red);
        private readonly Brush _yellowBrush = new SolidColorBrush(Colors.Yellow);
        private readonly Brush _greenBrush = new SolidColorBrush(Colors.Green);
        private readonly Brush _blackBrush = new SolidColorBrush(Colors.Black);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is LightState lightState)
            {
                return parameter switch
                {
                    "Red"    => lightState == LightState.Red ? _redBrush : _blackBrush,
                    "Yellow" => lightState == LightState.Yellow ? _yellowBrush : _blackBrush,
                    "Green"  => lightState == LightState.Green ? _greenBrush : _blackBrush,
                    _        => throw new ArgumentException($"{parameter} is an invalid value for {nameof(parameter)}")
                };
            }
            return null;
       }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new InvalidOperationException();
    }
}
