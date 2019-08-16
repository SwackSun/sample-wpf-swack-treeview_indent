using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace window_explorer.View
{
    public class TreeViewItemLeftMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = (TreeViewItem)value;
            if (item != null)
            {
                var itemshost = (ItemsPresenter)item.Template.FindName("ItemsHost", item);
                if (itemshost != null)
                {
                    Grid.SetColumn(itemshost, 1);
                    Grid.SetRow(itemshost, 1);
                    Grid.SetColumnSpan(itemshost, 2);
                    itemshost.Margin = new Thickness(-12, 0, 0, 0);
                }
            }
            return new Thickness(5, 0, 0, 0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
