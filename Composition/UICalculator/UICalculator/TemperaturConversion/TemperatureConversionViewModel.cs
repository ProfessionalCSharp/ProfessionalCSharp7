using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Composition
{
    public enum TempConversionType
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }

    public class TemperatureConversionViewModel : Observable
    {
        public TemperatureConversionViewModel()
        {
            CalculateCommand = new RelayCommand(OnCalculate);
        }

        public RelayCommand CalculateCommand { get; }

        public IEnumerable<string> TemperatureConversionTypes => Enum.GetNames(typeof(TempConversionType));

        private double ToCelsiusFrom(double t, TempConversionType conv)
        {
            switch (conv)
            {
                case TempConversionType.Celsius:
                    return t;
                case TempConversionType.Fahrenheit:
                    return (t - 32) / 1.8;
                case TempConversionType.Kelvin:
                    return (t - 273.15);
                default:
                    throw new ArgumentException("invalid enumeration value");
            }
        }

        private double FromCelsiusTo(double t, TempConversionType conv)
        {
            switch (conv)
            {
                case TempConversionType.Celsius:
                    return t;
                case TempConversionType.Fahrenheit:
                    return (t * 1.8) + 32;
                case TempConversionType.Kelvin:
                    return t + 273.15;
                default:
                    throw new ArgumentException("invalid enumeration value");
            }
        }

        private string _fromValue;
        public string FromValue
        {
            get => _fromValue;
            set => SetProperty(ref _fromValue, value);
        }

        private string _toValue;
        public string ToValue
        {
            get => _toValue; 
            set => SetProperty(ref _toValue, value);
        }

        private string _fromType;
        public string FromType
        {
            get => _fromType;
            set => SetProperty(ref _fromType, value);
        }

        private string _toType;
        public string ToType
        {
            get => _toType;
            set => SetProperty(ref _toType, value);
        }

        public void OnCalculate()
        {
            double celsius = ToCelsiusFrom(double.Parse(FromValue), Enum.Parse<TempConversionType>(FromType));
            double result = FromCelsiusTo(celsius, Enum.Parse<TempConversionType>(ToType));
            ToValue = result.ToString();
        }
    }
}
