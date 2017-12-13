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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ControlsSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RangeControlsPage : Page
    {
        public RangeControlsPage()
        {
            this.InitializeComponent();
            ShowProgress();
        }

        private void ShowProgress()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            int i = 0;
            timer.Tick += (sender, e) =>
            {
                progressBar1.Value = i++;
                if (i >= 100)
                {
                    i = 0;
                }
            };
            timer.Start();
        }

        private void OnGoBack()
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
