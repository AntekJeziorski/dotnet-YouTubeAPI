using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using YouTubeAPI;
using System.Data.Entity.Infrastructure;

using System.Windows.Forms.DataVisualization.Charting;
using dotnet_YouTubeAPI.Utils;
using System.Windows.Controls;
using System.Windows.Input;

namespace dotnet_YouTubeAPI.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// Non-parametric constructor for HomeView class. Initializes Home view for the aplication. 
        /// </summary>
        public HomeView()
        {
            InitializeComponent();
            PopulateHomeStats();
        }

        /// <summary>
        /// Creates lists of best trending artists and tracks to be displayed.
        /// </summary>
        void PopulateHomeStats()
        {
            using (var context = new YouTubeApiContext())
            {
                var mostVievedTrack = context.GetMostViewedTrack();
                mostWatchedVideoList.ItemsSource = mostVievedTrack.Take(1);

                var mostVievedAuthor = context.GetMostViewedAuthor();
                mostWatchedAuthorList.ItemsSource = mostVievedAuthor.Take(1);

                var mostLikedTrack = context.GetMostLikedTrack();
                mostLikedTrackList.ItemsSource = mostLikedTrack.Take(1);

                var mostVSubscribedAuthor = context.GetMostSubAuthor();
                mostSubscribedAuthor.ItemsSource = mostVSubscribedAuthor.Take(1);
            }
        }

        /// <summary>
        /// Refreshes vievs of lists of the best trending artists and tracks tracks after pressing a button.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Catched event.</param>
        private void RefreshHomeView(object sender, EventArgs e)
        {
            PopulateHomeStats();
        }
    }
}
