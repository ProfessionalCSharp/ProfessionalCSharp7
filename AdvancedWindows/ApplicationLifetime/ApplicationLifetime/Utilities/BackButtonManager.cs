using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace ApplicationLifetime.Utilities
{
    public class BackButtonManager : IDisposable
    {
        private SystemNavigationManager _navigationManager;
        private Frame _frame;

        public BackButtonManager(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
            _navigationManager = SystemNavigationManager.GetForCurrentView();
            _navigationManager.AppViewBackButtonVisibility = frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            _navigationManager.BackRequested += OnBackRequested;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (_frame.CanGoBack) _frame.GoBack();
            e.Handled = true;
        }

        public void Dispose()
        {
            _navigationManager.BackRequested -= OnBackRequested;
        }
    }
}
