using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeIntroWPF
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            var b = new Button
            {
                Content = "Click Me!"
            };
            b.Click += (sender, e) =>
            {
                b.Content = "clicked";
            };

            var w = new Window
            {
                Title = "Code Demo",
                Content = b
            };

            var app = new Application();
            app.Run(w);
        }

    }
}
