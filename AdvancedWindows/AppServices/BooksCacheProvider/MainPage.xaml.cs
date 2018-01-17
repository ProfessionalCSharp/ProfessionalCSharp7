using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BooksCacheProvider
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            PackageFamilyName = Package.Current.Id.FamilyName;
        }

        public string PackageFamilyName
        {
            get => (string)GetValue(PackageFamilyNameProperty);
            set => SetValue(PackageFamilyNameProperty, value);
        }

        public static readonly DependencyProperty PackageFamilyNameProperty =
            DependencyProperty.Register("PackageFamilyName", typeof(string), typeof(MainPage), new PropertyMetadata(string.Empty));
    }
}
