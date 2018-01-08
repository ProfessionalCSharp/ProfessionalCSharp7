using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlowDocumentSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnLoadData(object sender, RoutedEventArgs e)
        {
           
            var titleParagraph = new Paragraph();
            titleParagraph.Inlines.Add(new Run { Text = "Formula 1 Championship 2017", FontSize = 24, FontStyle = FontStyle.Oblique, FontStretch=FontStretch.ExtraExpanded });
            titleParagraph.Inlines.Add(new LineBreak());

            List<Paragraph> rows = F1Results.Results.Select(row =>
            {
                var line = new Paragraph();
                // position
                line.Inlines.Add(new Run { Text = $"{row[0]}. " });
                // name
                line.Inlines.Add(new Run { Text = $"{row[1]} " });
                // country
                var span1 = new Span();
                span1.Inlines.Add(new Run { Text = $"{row[2]} " });
                line.Inlines.Add(span1);
                // car
                line.Inlines.Add(new Run { Text = $"{row[3]} " });
                // points
                line.Inlines.Add(new Run { Text = $"{row[4]} " });

                line.Inlines.Add(new LineBreak());
                return line;

            }).ToList();
            var paragraph1 = new Paragraph();

            textBlock.Blocks.Add(titleParagraph);

            foreach (var row in rows)
            {
                textBlock.Blocks.Add(row);
                var separator = new Paragraph();
                separator.Inlines.Add()
                textBlock.Blocks.Add()
            }
        }
    }
}
