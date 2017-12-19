using Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace Templates
{
    public sealed partial class StyledList : Page
    {
        public ObservableCollection<Country> Countries { get; } = new ObservableCollection<Country>();

        public StyledList()
        {
            this.InitializeComponent();
            this.DataContext = this;
            var countries = new CountryRepository().GetCountries();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
    }
}
