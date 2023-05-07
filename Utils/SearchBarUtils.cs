using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace dotnet_YouTubeAPI.Utils
{
    /// <summary>
    /// Class used to style the appearance of the serachbar.
    /// </summary>
    public static class SearchBar {
        /// <summary>
        /// Makes placeholder in textbox invisible.
        /// </summary>
        /// <param name="SearchContent">String taken from the TextBlock field.</param>
        /// <param name="Placeholder">Message displayed when TextBlock is empty.</param>
        public static void SearchBarTextChange(TextBox SearchContent, TextBlock Placeholder)
        {
            if (SearchContent.Text.Length > 0)
                Placeholder.Visibility = Visibility.Collapsed;
            else
                Placeholder.Visibility = Visibility.Visible;
        }
    }
}
