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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinAppTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        public MainPage()
        {
            this.InitializeComponent();
            _timer.Tick += OnTick;
            _timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void OnTimer()
        {
            _timer.Start();
        }

        private void OnTick(object sender, object e)
        {
            double newAngle = rotate.Angle + 6;
            if (newAngle >= 360) newAngle = 0;
            rotate.Angle = newAngle;
        }

        private void OnStopTimer()
        {
            _timer.Stop();
        }
    }
}
