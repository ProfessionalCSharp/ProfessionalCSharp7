using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WPFEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnOpen(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Title = "Simple Editor - Open File",
                CheckPathExists = true,
                CheckFileExists = true,
                Filter = "Text files (*.txt)|*.txt|All files|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (dlg.ShowDialog() == true)
            {
                text1.Text = File.ReadAllText(dlg.FileName);
            }
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SaveFileDialog()
            {
                Title = "Simple Editor - Save As",
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files|*.*",
            };
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, text1.Text);
            }
        }
    }
}
