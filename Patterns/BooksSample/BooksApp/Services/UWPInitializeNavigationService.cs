using System;
using Windows.UI.Xaml.Controls;

namespace BooksApp.Services
{
    public class UWPInitializeNavigationService
    {
        public void SetFrame(Frame frame)
        {
            _frame = frame;
        }
        private Frame _frame;
        public Frame Frame => _frame ?? throw new ArgumentException("navigation not initialized");
    }
}
