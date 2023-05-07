using dotnet_YouTubeAPI.Utils;
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
    /// Interaction logic for ArtistView.xaml
    /// </summary>
    public partial class ArtistsView : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// Non-parametric constructor for ArtistsView class. Initializes Arist view on app. 
        /// </summary>
        public ArtistsView()
        {
            InitializeComponent();
            PopulateArtistsList();
        }

        /// <summary>
        /// Shows second window with detailed information about clicked arist.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Catched event.</param>
        private void OnListViewItemClicked(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListViewItem item = sender as System.Windows.Controls.ListViewItem;
            AuthorInfo data = item.Content as AuthorInfo;
            ArtistsInfoView subWindow = new ArtistsInfoView(data);
            subWindow.ReloadArtistsList += ReloadArtistsView;
            subWindow.Show();
        }

        /// <summary>
        /// Creates a list of artists to be displayed.
        /// </summary>
        void PopulateArtistsList()
        {
            using (var context = new YouTubeApiContext())
            {
                var artists = context.GetAuthorInfo();
                ArtisitsList.ItemsSource = artists;
            }
        }

        /// <summary>
        /// Refreshes list of tracked artists after pressing button.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Catched event.</param>
        private void ReloadArtistsView(object sender, EventArgs e)
        {
            PopulateArtistsList();
        }

        /// <summary>
        /// Refreshes list of tracked artists after the event occurance.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void RefreshArtistsListView(object sender, RoutedEventArgs e)
        {
            PopulateArtistsList();
        }

        /// <summary>
        /// Adds Artist to database with given ChannelID taken from textbox afetr clicking enter.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void SearchAuthorOnEnterKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var context = new YouTubeApiContext(); /* UCXuqSBlHAE6Xw-yeJA0Tunw */
                try
                {
                    var newAuthor = new YouTubeAPI.Author(searchBarContent.Text);
                    context.AddNewAuthor(newAuthor);
                    searchBarContent.Text = string.Empty;
                    var newAuthorEntry = new YouTubeAPI.AuthorsHistory(newAuthor.ChannelId);
                    context.AddNewAuthorHistoryEntry(newAuthorEntry);
                    PopulateArtistsList();
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
            SearchBar.SearchBarTextChange(searchBarContent, searchBarPlaceholder);
        }
    }
}
