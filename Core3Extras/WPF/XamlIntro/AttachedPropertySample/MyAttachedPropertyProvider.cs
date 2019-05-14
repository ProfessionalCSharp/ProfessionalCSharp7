using System.Windows;

namespace AttachedPropertyDemoWPF
{
    public class MyAttachedPropertyProvider : DependencyObject
    {
        public string MySample
        {
            get { return (string)GetValue(MySampleProperty); }
            set { SetValue(MySampleProperty, value); }
        }

        public static readonly DependencyProperty MySampleProperty =
            DependencyProperty.RegisterAttached(nameof(MySample), typeof(string), typeof(MyAttachedPropertyProvider),
                new PropertyMetadata(string.Empty));

        public static void SetMySample(UIElement element, string value) =>
            element.SetValue(MySampleProperty, value);

        public static string GetMySample(UIElement element) =>
            (string)element.GetValue(MySampleProperty);
    }

}
