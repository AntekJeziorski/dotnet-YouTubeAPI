using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using YouTubeAPI;
using System.Data.Entity.Infrastructure;

using System.Windows.Forms.DataVisualization.Charting;

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

        IList<Track> Tracks;
        
        public HomeView()
        {
            InitializeComponent();


            using (var context = new YouTubeApiContext())
            {
                Tracks = context.Tracks.ToList();
            }

            authorList.ItemsSource = Tracks;

            Console.WriteLine(Tracks);
        }
        
        private void Button_Click_Video(object sender, RoutedEventArgs e)
        {

            var context = new YouTubeApiContext();
            var tmp = context.GetAuthorInfo();
            Console.WriteLine(tmp);
            foreach (var item in tmp)
            {
                Console.WriteLine($"Author: {item.Author.ChannelId}, Latest Entry: {item.AuthorsHistory.AddTime}, View Count: {item.AuthorsHistory.ViewCount}");
            }
            context.UpdateAllTracks();
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

            //context.getTracksHistory("nLIp4wd0oXs");
        }

        private void Button_Click_Author(object sender, RoutedEventArgs e)
        {
            var context = new YouTubeApiContext(); /* UCXuqSBlHAE6Xw-yeJA0Tunw */
            try
            {
                var newAuthor = new YouTubeAPI.Author(textAuthorId.Text);
                context.AddNewAuthor(newAuthor);
                textAuthorId.Clear();
                var newAuthorEntry = new YouTubeAPI.AuthorsHistory(newAuthor.ChannelId);
                context.AddNewAuthorHistoryEntry(newAuthorEntry);
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
