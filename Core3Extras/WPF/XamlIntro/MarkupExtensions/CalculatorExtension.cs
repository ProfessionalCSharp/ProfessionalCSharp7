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
            IProvideValueTarget? provideValue =
                serviceProvider.GetService(typeof(IProvideValueTarget))
                as IProvideValueTarget;
            if (provideValue != null)
            {
                var host = provideValue.TargetObject as FrameworkElement;
                var prop = provideValue.TargetProperty as DependencyProperty;
            }
            return Operation switch
            {
                Operation.Add => (X + Y).ToString(),
                Operation.Subtract => (X - Y).ToString(),
                Operation.Multiply => (X * Y).ToString(),
                Operation.Divide => (X / Y).ToString(),
                _ => throw new ArgumentException("invalid operation")
            };
        }
    }
}
