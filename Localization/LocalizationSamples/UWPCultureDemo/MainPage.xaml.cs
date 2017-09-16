using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TreeViewControl;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
            ViewModel = new MainPageViewModel();
            this.InitializeComponent();

            
        }

        public MainPageViewModel ViewModel { get; }

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
