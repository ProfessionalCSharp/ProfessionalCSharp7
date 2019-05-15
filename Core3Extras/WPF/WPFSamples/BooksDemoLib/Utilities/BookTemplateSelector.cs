using BooksDemo.Models;
using System.Windows;
using System.Windows.Controls;

namespace BooksDemo.Utilities
{
    public class BookTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is Book)
            {
                var book = item as Book;
                switch (book.Publisher)
                {
                    case "Wrox Press":
                        return (container as FrameworkElement).FindResource("wroxTemplate") as DataTemplate;
                    case "For Dummies":
                        return (container as FrameworkElement).FindResource("dummiesTemplate") as DataTemplate;
                    default:
                        return (container as FrameworkElement).FindResource("bookTemplate") as DataTemplate;
                }
            }
            return null;          
        }
    }
}
