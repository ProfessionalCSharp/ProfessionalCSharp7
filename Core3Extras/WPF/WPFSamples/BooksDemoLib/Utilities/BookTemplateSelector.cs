using BooksDemo.Models;
using System.Windows;
using System.Windows.Controls;

namespace BooksDemo.Utilities
{
    public class BookTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object? item, DependencyObject? container)
        {
            if (item != null && item is Book book && container is FrameworkElement cont)
            {
                return book switch
                {
                    { Publisher: "Wrox Press" } => cont.FindResource("wroxTemplate") as DataTemplate,
                    { Publisher: "For Dummies" } => cont.FindResource("dummiesTemplate") as DataTemplate,
                    _ => cont.FindResource("bookTemplate") as DataTemplate
                };
            }

            return null;          
        }
    }
}
