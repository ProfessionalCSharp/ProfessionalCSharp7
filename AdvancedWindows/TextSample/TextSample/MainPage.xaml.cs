using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace TextSample
{

    public sealed partial class MainPage : Page
    {
        private List<Inline> _inlines = new List<Inline>();

        public MainPage()
        {
            this.InitializeComponent();           
        }

        //private void RefreshText3()
        //{
        //    text3.Inlines.Clear();
        //    foreach (var inline in _inlines)
        //    {
        //        text3.Inlines.Add(inline);
        //    }
        //}

        //private void OnAnalyze(object sender, RoutedEventArgs e)
        //{
        //    foreach (var text in text1.Inlines)
        //    {
        //        listResults.Items.Add($"{text.GetType().Name}, content: {text.ContentStart.Offset} - {text.ContentEnd.Offset}, element: {text.ElementStart.Offset} - {text.ElementEnd.Offset}");
        //    }
        //}


        //private void OnBold(object sender, RoutedEventArgs e)
        //{
        //    string selectedText = text2.SelectedText;
        //    TextPointer start = text2.SelectionStart;
        //    TextPointer end = text2.SelectionEnd;
        //    var inlines = SplitToThree(start.Parent as Run, start, end);
        //    int ix = text2.Inlines.IndexOf(start.Parent as Inline);
          
           
        //    foreach (var inline in inlines)
        //    {
        //        text2.Inlines.Insert(ix++, inline);
        //    }
        //    text2.Inlines.Insert(ix, new LineBreak());

        //  //  text2.Inlines.Remove(start.Parent as Inline);
           

        //}

        public Inline[] SplitToThree(Run run, TextPointer start, TextPointer end)
        {
            int startPos1 = 0;
            int length1 = start.Offset - run.ContentStart.Offset;
            string text1 = run.Text.Substring(startPos1, length1);
            int startPos2 = start.Offset - run.ContentStart.Offset;
            int length2 = end.Offset - start.Offset;
            string text2 = run.Text.Substring(startPos2, length2);
            int startPos3 = end.Offset - run.ContentStart.Offset;
            int length3 = run.ContentEnd.Offset - end.Offset;
            string text3 = run.Text.Substring(startPos3, length3);
            var run1 = new Run() { Text = text1 };
            var bold = new Bold();
            bold.Inlines.Add(new Run() { Text = text2 });
            var run3 = new Run() { Text = text3 };
            return new Inline[] { run1, bold, run3 };
        }
    }
}
