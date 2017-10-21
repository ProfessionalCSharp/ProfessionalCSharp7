using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWPCultureDemo
{
    public sealed partial class CultureDetailUC : UserControl
    {
        public CultureDetailUC()
        {
            this.InitializeComponent();
        }

        public CultureData CultureData
        {
            get { return (CultureData)GetValue(CultureDataProperty); }
            set { SetValue(CultureDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CultureData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CultureDataProperty =
            DependencyProperty.Register("CultureData", typeof(CultureData), typeof(CultureDetailUC), new PropertyMetadata(null));
    }
}
