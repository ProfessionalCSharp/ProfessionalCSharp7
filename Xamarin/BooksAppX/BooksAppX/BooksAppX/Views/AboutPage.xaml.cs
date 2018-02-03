using BooksAppX.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            ViewModel = ApplicationServices.Instance.ServiceProvider.GetService<AboutViewModel>();
            BindingContext = this;
        }

        public AboutViewModel ViewModel { get; }
    }
}