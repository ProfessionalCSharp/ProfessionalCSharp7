using DISampleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace XamarinClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            IServiceProvider container = (Application.Current as App).Container;
            ViewModel = container.GetService<ShowMessageViewModel>();
            this.BindingContext = this;

            container.GetService<IPageService>().Page = this;
        }

        public ShowMessageViewModel ViewModel { get; }
	}
}
