using BooksLib.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace BooksApp.Views
{
    public sealed partial class BookDetailUserControl : UserControl
    {
        public BookDetailUserControl()
        {
            this.InitializeComponent();
        }

        public ManageBooksViewModel ViewModel
        {
            get { return (ManageBooksViewModel)GetValue(ManageBooksViewModelProperty); }
            set { SetValue(ManageBooksViewModelProperty, value); }
        }

        public static readonly DependencyProperty ManageBooksViewModelProperty =
            DependencyProperty.Register("MyProperty", typeof(ManageBooksViewModel), typeof(BookDetailUserControl), new PropertyMetadata(null));


    }
}
