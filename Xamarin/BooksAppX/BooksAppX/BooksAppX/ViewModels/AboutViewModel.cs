using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BooksAppX.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            OpenWebCommand = new Command(() => 
                Device.OpenUri(new Uri("https://csharp.christiannagel.com")));

        }

        public ICommand OpenWebCommand { get; }
    }
}
