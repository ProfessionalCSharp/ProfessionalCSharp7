using PhasedBinding.Framework;
using PhasedBinding.Models;
using PhasedBinding.Services;

namespace PhasedBinding.ViewModels
{
    public class LunchMenuViewModel : BindableBase
    {
        private LunchMenuService _service = new LunchMenuService();

        public LunchMenuViewModel()
        {
        }

        public string IntroText { get; } = "A Lunch";

        private LunchMenu _lunchMenu;
        public LunchMenu LunchMenu
        {
            get => _lunchMenu;
            set => Set(ref _lunchMenu, value);
        }
    }
}
