using PhasedBinding.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhasedBinding.Services
{
    public class LunchMenuService
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

        public async Task<IEnumerable<LunchMenu>> GetLunchMenusAsync()
        {
            await Task.Delay(10000); // simulate a delay
            return _menusList;
        }
    }
}
