using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YouTubeAPI;
using System.Data.Entity.Infrastructure;

namespace dotnet_YouTubeAPI.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : System.Windows.Controls.UserControl
    {
        int newID;
        string newNickname;
        string newYtChannelID;
        IList<Author> authors;
        //IList<TracksHistory> authors;

        public HomeView()
        {
            InitializeComponent();

            using (var context = new YouTubeApiContext())
            {
                authors = context.Authors.ToList();
            }

            authorList.ItemsSource = authors;

            Console.WriteLine(authors);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Video(object sender, RoutedEventArgs e)
        {
            //var newVideo = new YouTubeAPI.Track(textVideoId.Text); /* nLIp4wd0oXs */
            //newVideo.GetViedoData();
            //textVideoId.Clear();

            //var context = new YouTubeApiContext();
            ////context.getAuthorsHistory("UCXuqSBlHAE6Xw-yeJA0Tunw");
            //try
            //{
            //    context.deleteAuthor(textAuthorId.Text);
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


            //var context = new YouTubeApiContext();
            //context.updateAllTracks();
            //context.getTracksHistory("nLIp4wd0oXs");

            var context = new YouTubeApiContext();
            var tmp = context.getAuthorInfo();
            Console.WriteLine(tmp);
            foreach (var item in tmp)
            {
                Console.WriteLine($"Author: {item.Author.ChannelId}, Latest Entry: {item.AuthorsHistory.AddTime}, View Count: {item.AuthorsHistory.ViewCount}");
            }
        }

        private void Button_Click_Author(object sender, RoutedEventArgs e)
        {
            var context = new YouTubeApiContext(); /* UCXuqSBlHAE6Xw-yeJA0Tunw */
            try
            {
                var newAuthor = new YouTubeAPI.Author(textAuthorId.Text);
                context.addNewAuthor(newAuthor);
                textAuthorId.Clear();
                var newAuthorEntry = new YouTubeAPI.AuthorsHistory(newAuthor.ChannelId);
                context.addNewAuthorHistoryEntry(newAuthorEntry);
                //context.getAuthorsHistory(newAuthor.ChannelId);
            }
            catch (DbUpdateException)
            {
                System.Windows.Forms.MessageBox.Show("Author already subscribed.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ////! For tracks
            //var context = new YouTubeApiContext(); /* UCXuqSBlHAE6Xw-yeJA0Tunw */
            //try
            //{
            //    var newTrack = new YouTubeAPI.Track(textAuthorId.Text);
            //    context.addNewTrack(newTrack);
            //    textAuthorId.Clear();
            //    var newTrackEntry = new YouTubeAPI.TracksHistory(newTrack.VideoId);
            //    context.addNewTrackHistoryEntry(newTrackEntry);
            //    //context.getAuthorsHistory(newAuthor.ChannelId);
            //}
            //catch (DbUpdateException)
            //{
            //    System.Windows.Forms.MessageBox.Show("Author already subscribed.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
