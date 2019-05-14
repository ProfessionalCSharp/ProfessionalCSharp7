using System;
using System.Windows;
using System.Windows.Markup;

namespace MarkupExtensionsWPF
{
    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    [MarkupExtensionReturnType(typeof(string))]
    public class CalculatorExtension : MarkupExtension
    {
        public CalculatorExtension()
        {
        }
        public double X { get; set; }
        public double Y { get; set; }
        public Operation Operation { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget provideValue =
                serviceProvider.GetService(typeof(IProvideValueTarget))
                as IProvideValueTarget;
            if (provideValue != null)
            {
                var host = provideValue.TargetObject as FrameworkElement;
                var prop = provideValue.TargetProperty as DependencyProperty;
            }
            double result = 0;
            switch (Operation)
            {
                case Operation.Add:
                    result = X + Y;
                    break;
                case Operation.Subtract:
                    result = X - Y;
                    break;
                case Operation.Multiply:
                    result = X * Y;
                    break;
                case Operation.Divide:
                    result = X / Y;
                    break;
                default:
                    throw new ArgumentException("invalid operation");
            }
            return result.ToString();
        }
    }
}
