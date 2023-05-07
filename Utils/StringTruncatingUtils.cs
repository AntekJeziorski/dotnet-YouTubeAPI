using System;
using System.Globalization;
using System.Windows.Data;

namespace dotnet_YouTubeAPI.Utils
{
    /// <summary>
    /// Class of converter used for truncating long string to only one paragraph.
    /// </summary>
    public class StringTruncationConverter : IValueConverter
    {
        /// <summary>
        /// Truncates video description to only one paragraph.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            string inputString = value.ToString();
            return inputString.Split('\n')[0];
        }

        /// <summary>
        /// This converter does not provide conversion back from ordinal position to list view item.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
