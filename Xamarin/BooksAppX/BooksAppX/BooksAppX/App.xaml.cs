using BooksAppX.Services;
using BooksAppX.ViewModels;
using BooksLib.Models;
using BooksLib.Services;
using BooksLib.ViewModels;
using Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace BooksAppX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BooksAppX.Views.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            ApplicationServices.Instance.ServiceProvider.GetService<INavigationService>().UseNavigation = true; // always use navigation
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
