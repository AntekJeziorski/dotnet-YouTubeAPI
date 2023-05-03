using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void OnEnterKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Console.WriteLine(txtUserName.Text);
                var context = new YouTubeApiContext(); /* UCXuqSBlHAE6Xw-yeJA0Tunw */
                try
                {
                    var newTrack = new YouTubeAPI.Track(txtUserName.Text);
                    context.addNewTrack(newTrack);
                    txtUserName.Text = string.Empty;
                    var newTrackEntry = new YouTubeAPI.TracksHistory(newTrack.VideoId);
                    context.addNewTrackHistoryEntry(newTrackEntry);
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
                var tracks = context.GetAllTracks();
                listView.ItemsSource = tracks;
            }
        }
    }
    public class RowNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result =1;
            
            if (value != null && int.TryParse(value.ToString(), out result))
            {
                return ++result;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
