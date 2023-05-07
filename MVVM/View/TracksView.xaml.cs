using dotnet_YouTubeAPI.Utils;
using System;
using System.Data.Entity.Infrastructure;
using System.Windows;
using System.Windows.Controls;
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
        /// Non-parametric constructor for TracksView class. Initializes Tracks view for the aplication. 
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
            subWindow.ReloadTracksList += ReloadTracksView;
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
        /// Refreshes list of tracked tracks after pressing button.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Catched event.</param>
        private void ReloadTracksView(object sender, EventArgs e)
        {
            PopulateTrackList();
        }

        /// <summary>
        /// Refreshes list of tracked artists after the event occurance.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void RefreshTrackListView(object sender, RoutedEventArgs e)
        {
            PopulateTrackList();
        }

        /// <summary>
        /// Adds Track to database with given VideoID taken from textbox afetr clicking enter.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void SearchOnEnterKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var context = new YouTubeApiContext();
                try
                {
                    var newTrack = new YouTubeAPI.Track(searchBarContent.Text);
                    context.AddNewTrack(newTrack);
                    searchBarContent.Text = string.Empty;
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
                    System.Windows.Forms.MessageBox.Show("Specified Track does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SearchBar.SearchBarTextChange(searchBarContent, searchBarPlaceholder);
        }
    }
}
