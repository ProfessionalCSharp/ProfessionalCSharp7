using System;
using Windows.UI.Xaml;

namespace StylesAndResources
{
    public static class FrameworkElementExtensions
    {
        public static object TryFindResource(this FrameworkElement e, string key)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (e.Resources.ContainsKey(key))
            {
                return e.Resources[key];
            }
            else if (e.Parent is FrameworkElement parent)
            {
                return TryFindResource(parent, key);
            }
            else
            {
                return null;
            }
        }
    }
}
