using PhasedBinding.Models;
using PhasedBinding.Services;
using PhasedBinding.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhasedBinding
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<LunchMenuViewModel> _lunchViewModels = new ObservableCollection<LunchMenuViewModel>(Enumerable.Range(0, 8).Select(x => new LunchMenuViewModel()));
        private LunchMenuService _service = new LunchMenuService();
        public ObservableCollection<LunchMenuViewModel> LunchViewModels => _lunchViewModels;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<LunchMenu> lunchMenus = (await _service.GetLunchMenusAsync()).ToList();
                for (int i = 0; i < 8; i++)
                {
                    _lunchViewModels[i].LunchMenu = lunchMenus[i];

                }
            }
            catch (Exception ex)
            {
                // TODO: log the exception
                throw;
            }
        }
    }
}
