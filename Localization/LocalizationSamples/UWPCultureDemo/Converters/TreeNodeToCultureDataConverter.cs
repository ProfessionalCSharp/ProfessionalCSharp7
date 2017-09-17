using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewControl;
using Windows.UI.Xaml.Data;

namespace UWPCultureDemo.Converters
{
    public class TreeNodeToCultureDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TreeNode node)
            {
                return node.Data as CultureData;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
