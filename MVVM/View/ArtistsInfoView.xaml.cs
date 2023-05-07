using System;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using YouTubeAPI;

namespace dotnet_YouTubeAPI.MVVM.View
{
    /// <summary>
    /// Interaction logic for ArtistsInfoView.xaml
    /// </summary>
    public partial class ArtistsInfoView : Window
    {
        /// <summary>
        /// Gets or sets AuthorInfo object for selected author from list.
        /// </summary>
        public AuthorInfo InspectedArtist { get; set; }

        /// <summary>
        /// Event that is raised when the list of artists needs to be reloaded.
        /// </summary>
        public event EventHandler ReloadArtistsList;

        /// <summary>
        /// Constructor for ArtistsInfoView class. Initializes window with detalied information about selected artist.
        /// </summary>
        /// <param name="data">Artist information to be displayed.</param>
        public ArtistsInfoView(AuthorInfo data)
            {
                InspectedArtist = data;
                this.DataContext = InspectedArtist;
                InitializeComponent();
            }

        /// <summary>
        /// Deletes Artist and whole Author's history from database after clicking unsubscribe button and cofirming it in messagebox.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void Button_Click_DeleteArtist(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.None;
            result = System.Windows.MessageBox.Show("Do you really want to unsubscribe this author?", "Unsubscribe", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                Window currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                try
                {
                    using (var context = new YouTubeApiContext())
                    {
                        context.DeleteAuthor(InspectedArtist.Author.ChannelId);
                    }
                    ReloadArtistsList?.Invoke(this, EventArgs.Empty);
                    currentWindow.Close();
                }
                catch (DbUpdateException)
                {
                    System.Windows.Forms.MessageBox.Show("Author already deleted.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentWindow.Close();
                }
            }
        }

        /// <summary>
        /// Renders a scatter plot that represents the number of total channel views over time.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void PopulateViewsChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var author = context.GetAuthorHistory(InspectedArtist.Author.ChannelId);
            var series = new Series("Data");
            foreach (var item in author)
            {
                series.Points.AddXY(item.AddTime.Millisecond, item.ViewCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Point;

            // Add the series to the chart
            ViewsChart.Series.Add(series);
            ViewsChart.Series["Data"].Points[0].Color = System.Drawing.Color.Red;

            // Customize the chart appearance
            ViewsChart.Titles.Add("Likes number per history update");
            ViewsChart.ChartAreas[0].AxisX.Title = "Time";
            ViewsChart.ChartAreas[0].AxisY.Title = "Likes Number";
        }

        /// <summary>
        /// Renders a scatter plot that represents the number of total channel subscribers over time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateSubChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var author = context.GetAuthorHistory(InspectedArtist.Author.ChannelId);
            var series = new Series("Data");
            foreach (var item in author)
            {
                series.Points.AddXY(item.AddTime.Millisecond, item.SubCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Point;

            // Add the series to the chart
            SubChart.Series.Add(series);
            SubChart.Series["Data"].Points[0].Color = System.Drawing.Color.Red;

            // Customize the chart appearance
            SubChart.Titles.Add("Likes number per history update");
            SubChart.ChartAreas[0].AxisX.Title = "Time";
            SubChart.ChartAreas[0].AxisY.Title = "Likes Number";
        }

        /// <summary>
        /// Renders a scatter plot that represents the number of all videos published on the channel over time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateVideosChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var author = context.GetAuthorHistory(InspectedArtist.Author.ChannelId);
            var series = new Series("Data");
            foreach (var item in author)
            {
                series.Points.AddXY(item.AddTime.Millisecond, item.VideoCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Point;

            // Add the series to the chart
            CommentsChart.Series.Add(series);
            CommentsChart.Series["Data"].Points[0].Color = System.Drawing.Color.Red;

        // Customize the chart appearance
            CommentsChart.Titles.Add("Likes number per history update");
            CommentsChart.ChartAreas[0].AxisX.Title = "Time";
            CommentsChart.ChartAreas[0].AxisY.Title = "Likes Number";
        }
    }
}

