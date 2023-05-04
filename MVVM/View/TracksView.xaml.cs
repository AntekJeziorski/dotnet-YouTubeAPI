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
        public TracksView()
        {
            InitializeComponent();
            PopulateCollection();
        }

        private void OnListViewItemClicked(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListViewItem item = sender as System.Windows.Controls.ListViewItem;
            TrackInfo data = item.Content as TrackInfo;
            TrackInfoView subWindow = new TrackInfoView(data);
            subWindow.Show();
        }

        private void OnEnterKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                    PopulateCollection();
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

        private void SearchBarTextChange(object sender, TextChangedEventArgs e)
        {
            if (txtUserName.Text.Length > 0)
                tbUsername.Visibility = Visibility.Collapsed;
            else
                tbUsername.Visibility = Visibility.Visible;
        }

        void PopulateCollection()
        {
            using (var context = new YouTubeApiContext())
            {
                var tracks = context.GetTrackInfo();
                listView.ItemsSource = tracks;
            }
        }
    }
    public class RowNumberConverter : IValueConverter
    {
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

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // This converter does not provide conversion back from ordinal position to list view item
            throw new System.InvalidOperationException();
        }
    }
}
