using PhasedBinding.Models;
using PhasedBinding.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhasedBinding.ViewModels
{
    public class LunchMenuViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void Set<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
