using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Alessia.Models;
using System;

namespace Alessia.Helpers
{
    public sealed class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ListBoxItem item = value as ListBoxItem;
            ListBox list = ItemsControl.ItemsControlFromItemContainer(item) as ListBox;

#if DEBUG
            System.Diagnostics.Debug.WriteLine("Item generated");
#endif

            return list.IndexFromContainer(item) + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
