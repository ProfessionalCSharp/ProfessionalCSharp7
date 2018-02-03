
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Extensions.DependencyInjection;
using BooksAppX.Services;
using BooksLib.Services;
using Framework.Services;
using System.Threading.Tasks;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage ()
        {
            InitializeComponent();

            (Application.Current as App).AppServices.GetService<XamarinInitializeNavigationService>().SetNavigation(navigationPage.Navigation, PageNames.BooksPage);
        }

        protected override bool OnBackButtonPressed()
        {
            OnBackButtonAsync();
            return true;
        }

        private async void OnBackButtonAsync()
        {
            await (Application.Current as App).AppServices.GetService<INavigationService>().GoBackAsync();
        }
    }
}