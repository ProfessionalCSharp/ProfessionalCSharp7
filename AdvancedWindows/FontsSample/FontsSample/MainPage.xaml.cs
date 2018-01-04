using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FontsSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
            get { return (string)GetValue(SelectedFontProperty); }
            set { SetValue(SelectedFontProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedFont.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFontProperty =
            DependencyProperty.Register("SelectedFont", typeof(string), typeof(MainPage), new PropertyMetadata(string.Empty));



    }
}
