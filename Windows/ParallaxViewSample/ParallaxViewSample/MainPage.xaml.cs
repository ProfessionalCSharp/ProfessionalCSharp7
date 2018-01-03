using ParallaxViewSample.Models;
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

namespace ParallaxViewSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IEnumerable<LunchMenu> _menusList = new List<LunchMenu>()
        {
            new LunchMenu { MenuId = 1, Text = "Chicken Salad", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/Backhendelsalat"},
            new LunchMenu { MenuId = 2, Text = "Cordon Bleu", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/CordonBleu_250"},
            new LunchMenu { MenuId = 3, Text = "Wiener Schnitzel", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/WienerSchnitzel_250"},
            new LunchMenu { MenuId = 4, Text = "Lasagne", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/Lasagne_250"},
            new LunchMenu { MenuId = 5, Text = "Lentils with bacon and dumplings", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/Linsen_250"},
            new LunchMenu { MenuId = 6, Text = "Schweinslungenbraten", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/Schweinslungenbraten_250"},
            new LunchMenu { MenuId = 7, Text = "Spätzle", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/Spinatspaetzle_250"},
            new LunchMenu { MenuId = 8, Text = "Topfennockerl", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/Topfennockerl_250"},
            new LunchMenu { MenuId = 9, Text = "Fried trout with potatoes", ImageUrl = "https://kantinem101.blob.core.windows.net/menuimages/forelle_250"},
        };

        public IEnumerable<LunchMenu> Menus => _menusList;

        public MainPage()
        {
         //   _menusList = _menusList.Concat(_menusList).Concat(_menusList).Concat(_menusList);

            this.InitializeComponent();
        }
    }
}
