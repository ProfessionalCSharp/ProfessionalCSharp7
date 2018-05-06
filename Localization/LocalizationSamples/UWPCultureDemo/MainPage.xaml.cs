using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPCultureDemo
{
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

        private void OnSelectionChanged(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is TreeViewNode node && node.Content is CultureData cd)
            {
                ViewModel.SelectedCulture = cd;                
            }
        }

        public CulturesViewModel ViewModel { get; } = new CulturesViewModel();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            void AddSubNodes(TreeViewNode parent)
            {
                if (parent.Content is CultureData cd && cd.SubCultures != null)
                {
                    foreach (var culture in cd.SubCultures)
                    {
                        var node = new TreeViewNode
                        {
                            Content = culture
                        };
                        parent.Children.Add(node);

                        foreach (var subCulture in culture.SubCultures)
                        {
                            AddSubNodes(node);
                        }
                    }
                }
            }

            base.OnNavigatedTo(e);
            var rootNodes = ViewModel.RootCultures.Select(cd => new TreeViewNode
            {
                Content = cd
            });

            foreach (var node in rootNodes)
            {
                treeView1.RootNodes.Add(node);
                AddSubNodes(node);
            }
        }
    }
}
