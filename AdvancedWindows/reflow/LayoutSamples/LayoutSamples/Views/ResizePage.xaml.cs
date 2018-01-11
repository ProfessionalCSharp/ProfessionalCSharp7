using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LayoutSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResizePage : Page
    {
        public ResizePage()
        {
            this.InitializeComponent();
        }

        private async void OnOpenFile(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".rtf");
            StorageFile file = await picker.PickSingleFileAsync();
            using (var winstream = await file.OpenReadAsync())
            {
                StreamReader reader = new StreamReader(winstream.AsStream());
                string content = reader.ReadToEnd();

                this.text1.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, content);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
              Frame.CanGoBack ? AppViewBackButtonVisibility.Visible :
              AppViewBackButtonVisibility.Collapsed;

            base.OnNavigatedTo(e);
        }
    }
}

