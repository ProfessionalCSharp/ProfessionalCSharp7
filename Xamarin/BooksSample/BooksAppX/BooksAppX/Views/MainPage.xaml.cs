using BooksAppX.Services;
using System;
using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            var initializeNavigation = (Application.Current as App).AppServices.GetService<InitializeNavigationService>();
            initializeNavigation.SetNavigation(booksPage.Navigation);
        }
    }
}