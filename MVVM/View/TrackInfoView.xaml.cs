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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TrackInfoView : Window
    {
        /// <summary>
        /// Gets or sets TracksInfo object for selected track from list.
        /// </summary>
        public TrackInfo InspectedTrack { get; set; }

        /// <summary>
        /// Event that is raised when the list of tracks needs to be reloaded.
        /// </summary>
        public event EventHandler ReloadTracksList;

        /// <summary>
        /// Constructor for TrackInfoView class. Initializes window with detalied information about selected video.
        /// </summary>
        /// <param name="data"></param>
        public TrackInfoView(TrackInfo data)
        {
            InspectedTrack = data;
            this.DataContext = InspectedTrack;
            InitializeComponent();
        }

        /// <summary>
        /// Deletes track from database after clicking unsubscribe button and cofirming it in messagebox.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void Button_Click_DeleteTrack(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.None;
            result = System.Windows.MessageBox.Show("Do you really want to unsubscribe this track?", "Unsubscribe", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                Window currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                try
                {
                    using (var context = new YouTubeApiContext())
                    {
                        context.DeleteTrack(InspectedTrack.Track.VideoId);
                    }
                    ReloadTracksList?.Invoke(this, EventArgs.Empty);
                    currentWindow.Close();
                }
                catch (DbUpdateException)
                {
                    System.Windows.Forms.MessageBox.Show("Track already deleted.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentWindow.Close();
                }
            }
        }

        /// <summary>
        /// Renders charts which represents history of views.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Caught event.</param>
        private void PopulateViewsChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var track = context.GetTracksHistory(InspectedTrack.Track.VideoId);
            var series = new Series("Data");
            foreach (var item in track)
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
        /// Renders charts which represents history of likes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateLikesChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var track = context.GetTracksHistory(InspectedTrack.Track.VideoId);
            var series = new Series("Data");
            foreach (var item in track)
            {
                series.Points.AddXY(item.AddTime.Millisecond, item.ViewCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Point;

            // Add the series to the chart
            LikesChart.Series.Add(series);
            LikesChart.Series["Data"].Points[0].Color = System.Drawing.Color.Red;

            // Customize the chart appearance
            LikesChart.Titles.Add("Likes number per history update");
            LikesChart.ChartAreas[0].AxisX.Title = "Time";
            LikesChart.ChartAreas[0].AxisY.Title = "Likes Number";
        }

        /// <summary>
        /// Renders charts which represents history of comments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateCommentsChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var track = context.GetTracksHistory(InspectedTrack.Track.VideoId);
            
            var series = new Series("Data");
            foreach (var item in track)
            {
                series.Points.AddXY(item.AddTime.Millisecond, item.CommentCount);
            }
            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Point;

            // Add the series to the chart
            CommentsChart.Series.Add(series);
            CommentsChart.Series["Data"].Points[0].Color = System.Drawing.Color.Red;

            // Customize the chart appearance
            CommentsChart.Titles.Add("Comments number per history update");
            CommentsChart.ChartAreas[0].AxisX.Title = "Time";
            CommentsChart.ChartAreas[0].AxisY.Title = "Comments Number";
        }
    }

    /// <summary>
    /// Class used for truncating video description to only one paragraph.
    /// </summary>
    public class StringTruncationConverter : IValueConverter
    {
        /// <summary>
        /// Truncates video description to only one paragraph.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            string inputString = value.ToString();
            return inputString.Split('\n')[0];
        }

        /// <summary>
        /// This converter does not provide conversion back from ordinal position to list view item.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
  
}
