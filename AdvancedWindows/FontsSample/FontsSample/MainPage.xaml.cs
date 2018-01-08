using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FontsSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SelectedFont = FontNames.First();
        }

        // good for headings and UI elements
        private readonly string[] _sansSerifFontNames = { "Arial", "Calibri", "Consolas", "Segoe UI", "Segoe UI Historic", "Selawik", "Verdana" };

        // good for large amounts of text
        private readonly string[] _serifFontNames = { "Cambria", "Courier New", "Georgia", "Times New Roman" };
        // symbols and icons
        private readonly string[] _symbolsAndIconFontNames = { "Segoe MDL2 Assets", "Segoe UI Emoji", "Segoe UI Symbol" };
        // non-latin
        private readonly string[] _nonLatinFontNames = { "Ebrima", "Gadugi", "Javanese", "Leelawadee UI", "Malgun Gothic", "Microsoft Himalaya", "Microsoft JhengHei UI", "Microsoft PhagsPa", "Microsoft Tai Le", "Microsoft YaHei UI", "Microsoft Yi Baiti", "Mongolian Baiti", "MV Boli", "Myanmar Text", "Nirmala UI", "SimSun", "Yu Gothic", "Yu Gothic UI" };

        private string[] allFonts;
        public string[] FontNames => allFonts ?? (allFonts = _sansSerifFontNames.Concat(_serifFontNames).Concat(_symbolsAndIconFontNames).Concat(_nonLatinFontNames).ToArray());

        public string SelectedFont
        {
            get => (string)GetValue(SelectedFontProperty);
            set => SetValue(SelectedFontProperty, value);
        }

        public static readonly DependencyProperty SelectedFontProperty =
            DependencyProperty.Register("SelectedFont", typeof(string), typeof(MainPage), new PropertyMetadata(string.Empty));
    }
}
