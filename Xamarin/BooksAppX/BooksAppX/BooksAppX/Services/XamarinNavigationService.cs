using BooksAppX.Views;
using BooksLib.Services;
using Framework.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace BooksAppX.Services
{
    public class XamarinNavigationService : INavigationService
    {
        private Dictionary<string, Func<Page>> _pages = new Dictionary<string, Func<Page>>
        {
            [PageNames.BooksPage] = () => new BooksPage(),
            [PageNames.BookDetailPage] = () => new BookDetailPage()
        };

        private Stack<string> _pageStack = new Stack<string>();

        private bool _initialized = false;
        private void Initialize()
        {
            if (!_initialized)
            {
                _navigation = _initializeNavigation.Navigation ?? throw new InvalidOperationException($"{nameof(_initializeNavigation.Navigation)} not initialized");
                _pageStack.Push(_initializeNavigation.IntialPage);
                _initialized = true;
            }
        }

        private string initialPage;
        private INavigation _navigation;
        private INavigation Navigation
        {
            get => _navigation;
        }

        public string CurrentPage
        {
            get
            {
                Initialize();
                return _pageStack.Peek();
            }
        }

        public bool UseNavigation { get; set; }

        private readonly XamarinInitializeNavigationService _initializeNavigation;

        public XamarinNavigationService(XamarinInitializeNavigationService initializeNavigation) =>
            _initializeNavigation = initializeNavigation ?? throw new ArgumentNullException(nameof(initializeNavigation));

        public Task GoBackAsync()
        {
            _pageStack.Pop();
            return Navigation.PopAsync();
        }

        public Task NavigateToAsync(string pagename)
        {
            _pageStack.Push(pagename);
            return Navigation.PushAsync(_pages[pagename]());
        }
    }
}
