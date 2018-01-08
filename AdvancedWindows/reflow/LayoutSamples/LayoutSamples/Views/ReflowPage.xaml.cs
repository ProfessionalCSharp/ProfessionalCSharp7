using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LayoutSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReflowPage : Page
    {
        public ReflowPage()
        {
            this.InitializeComponent();
        }

        private async void OnOpenFile(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".rtf");
            StorageFile file = await picker.PickSingleFileAsync();
            using (var winstream = await file.OpenReadAsync())
            {
                StreamReader reader = new StreamReader(winstream.AsStream());
                string content = reader.ReadToEnd();

                text1.Blocks.Add(new Paragraph()
                {
                    
                });
                // this.text1.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, content);
            }
        }
    }
}
