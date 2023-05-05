using System;
using System.Data.Entity.Infrastructure;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using YouTubeAPI;

namespace dotnet_YouTubeAPI.MVVM.View
{
    /// <summary>
    /// Interaction logic for TracksView.xaml
    /// </summary>
    public partial class TracksView : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// Non-parametric constructor for TracksView class. Initializes Tracks view on app. 
        /// </summary>
        public TracksView()
        {
            InitializeComponent();
            PopulateTrackList();
        }

        /// <summary>
        /// Shows second window with detailed information about clicked track.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Catched event.</param>
        private void OnListViewItemClicked(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListViewItem item = sender as System.Windows.Controls.ListViewItem;
            TrackInfo data = item.Content as TrackInfo;
            TrackInfoView subWindow = new TrackInfoView(data);
            subWindow.ReloadTracksList += TrackInfoView_ReloadMainWindow;
            subWindow.Show();
        }

        /// <summary>
        /// Creates list of tracks for display.
        /// </summary>
        void PopulateTrackList()
        {
            using (var context = new YouTubeApiContext())
            {
                var tracks = context.GetTrackInfo();
                listView.ItemsSource = tracks;
            }
        }

        /// <summary>
        /// Refreshes main window.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Catched event.</param>
        private void TrackInfoView_ReloadMainWindow(object sender, EventArgs e)
        {
            PopulateTrackList();
        }

        /// <summary>
        /// Adds track to database with given VideoID in textbox afetr clicking enter.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void SearchOnEnterKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var context = new YouTubeApiContext(); /* UCXuqSBlHAE6Xw-yeJA0Tunw */
                try
                {
                    var newTrack = new YouTubeAPI.Track(txtUserName.Text);
                    context.AddNewTrack(newTrack);
                    txtUserName.Text = string.Empty;
                    var newTrackEntry = new YouTubeAPI.TracksHistory(newTrack.VideoId);
                    context.AddNewTrackHistoryEntry(newTrackEntry);
                    PopulateTrackList();
                }
                catch (DbUpdateException)
                {
                    System.Windows.Forms.MessageBox.Show("Author already subscribed.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        /// <summary>
        /// Makes placeholder in textbox invisible.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void SearchBarTextChange(object sender, TextChangedEventArgs e)
        {
            if (txtUserName.Text.Length > 0)
                tbUsername.Visibility = Visibility.Collapsed;
            else
                tbUsername.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Refreshes list of tracked videos.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void RefreshTrackListView(object sender, RoutedEventArgs e)
        {
            PopulateTrackList();
        }
    }

    /// <summary>
    /// Class used for converting numbers for numbering the tarcks on list.
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
