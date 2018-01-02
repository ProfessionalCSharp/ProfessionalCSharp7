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
    public sealed partial class BookItemUserControl : UserControl
    {
        public BookItemUserControl()
        {
            this.InitializeComponent();
        }

        public BookItemViewModel BookViewModel
        {
            get { return (BookItemViewModel)GetValue(BookViewModelProperty); }
            set { SetValue(BookViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BookViewModelProperty =
            DependencyProperty.Register("BookViewModel", typeof(BookItemViewModel), typeof(BookItemUserControl), new PropertyMetadata(null));

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);

            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse || e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
            {
                bool result = VisualStateManager.GoToState(this, "HoverButtonsShown", true);
            }
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);

            bool result = VisualStateManager.GoToState(this, "HoverButtonsHidden", true);
        }

    }
}
