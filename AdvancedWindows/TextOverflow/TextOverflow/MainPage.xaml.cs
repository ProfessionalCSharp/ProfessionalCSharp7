using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace TextOverflow
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoadData;
        }

        private void OnLoadData(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                textBlock.Blocks.Add(GetBlock());
            }
        }

        public Block GetBlock()
        {
            var paragraph = new Paragraph { TextAlignment = TextAlignment.Justify };
            foreach (var inline in GetInlineElements())
            {
                paragraph.Inlines.Add(inline);
            }
            return paragraph;
        }

        public IEnumerable<Inline> GetInlineElements()
        {
            var inlines = new List<Inline>();
            var header = new Bold();
            header.Inlines.Add(new Run { Text = "Lorem ipsum" });
            inlines.Add(header);
            inlines.Add(new LineBreak());
            inlines.Add(new Run { Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. " });
            inlines.Add(new LineBreak());
            return inlines;
        }
    }
}
