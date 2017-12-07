using Windows.UI.Xaml;

namespace DependencyObjectSample
{
    public class MyDependencyObject : DependencyObject
    {
        public int Value
        {
            get => (int)GetValue(ValueProperty); 
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(MyDependencyObject), new PropertyMetadata(0, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int oldValue = (int)e.OldValue;
            int newValue = (int)e.NewValue;
            //...
        }

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(int),
                typeof(MyDependencyObject), new PropertyMetadata(0));

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int),
              typeof(MyDependencyObject), new PropertyMetadata(100));
    }
}
