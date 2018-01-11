using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace BooksApp.Services
{
    public class UWPInitializeNavigationService
    {
        public void Initialize(Frame frame, Dictionary<string, Type> pages)
        {
            Frame = frame ?? throw new ArgumentNullException(nameof(frame));
            Pages = pages ?? throw new ArgumentNullException(nameof(pages));
        }
        public Frame Frame { get; private set; }
        public Dictionary<string, Type> Pages { get; private set; }
    }
}
