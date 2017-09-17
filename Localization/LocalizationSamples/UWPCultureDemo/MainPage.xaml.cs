using System.Linq;
using TreeViewControl;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPCultureDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ViewModel = new CulturesViewModel();
            this.InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedCulture =
                (treeView1.SelectedItems?.FirstOrDefault() as TreeNode)?.Data as CultureData;
        }

        public CulturesViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            void AddSubNodes(TreeNode parent)
            {
                if (parent.Data is CultureData cd && cd.SubCultures != null)
                {
                    foreach (var culture in cd.SubCultures)
                    {
                        var node = new TreeNode
                        {
                            Data = culture,
                            ParentNode = parent
                        };
                        parent.Add(node);

                        foreach (var subCulture in culture.SubCultures)
                        {
                            AddSubNodes(node);
                        }
                    }
                }
            }

            base.OnNavigatedTo(e);
            var rootNodes = ViewModel.RootCultures.Select(cd => new TreeNode
            {
                Data = cd
            });

            foreach (var node in rootNodes)
            {
                treeView1.RootNode.Add(node);
                AddSubNodes(node);
            }
        }
    }
}
