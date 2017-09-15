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
using Wrox.ProCSharp.Composition;

namespace UWPCalculatorHost.Views
{
    public sealed partial class CalculatorUI : UserControl
    {
        public CalculatorUI()
        {
            this.InitializeComponent();
            ViewModel = new CalculatorViewModel();
            ViewModel.Init(typeof(Calculator), typeof(SubtractOperation), typeof(AddOperation), typeof(SlowAddOperation));
        }

        public CalculatorViewModel ViewModel { get; }

        public void OnNumberClick(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                ViewModel.Input += b.Content.ToString();
            }
        }

        public void DefineOperation(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                ViewModel.CurrentOperation = b.Tag as IOperation;
            }
        }
    }
}
