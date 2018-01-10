using BooksAppX.ViewModels;
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
            this.BindingContext = this;
        }

        public AboutViewModel ViewModel { get; set; }
    }
}