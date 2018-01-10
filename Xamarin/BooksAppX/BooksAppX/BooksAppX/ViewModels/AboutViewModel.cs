using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BooksAppX.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            WebPageCommand = new Command(() => Device.OpenUri(new Uri("https://csharp.christiannagel.com")));
        }

        public ICommand WebPageCommand { get; }
    }
}
