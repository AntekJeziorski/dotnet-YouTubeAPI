using System.Windows.Controls;
using System.Windows.Data;

namespace dotnet_YouTubeAPI.Utils
{

    /// <summary>
    /// Class of converter used for automatic numbering list elements.
    /// </summary>
    public class RowNumberConverter : IValueConverter
    {
        /// <summary>
        /// Gives each tarck its own number on the list.
        /// </summary>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Controls.ListViewItem lvi = value as System.Windows.Controls.ListViewItem;
            int ordinal = 0;

            if (lvi != null)
            {
                System.Windows.Controls.ListView lv = ItemsControl.ItemsControlFromItemContainer(lvi) as System.Windows.Controls.ListView;
                ordinal = lv.ItemContainerGenerator.IndexFromContainer(lvi) + 1;
            }

            return ordinal;
        }

        /// <summary>
        /// This converter does not provide conversion back from ordinal position to list view item.
        /// </summary>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.InvalidOperationException();
        }
    }
}

