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
        public TrackInfo InspectedTrack { get; set; }

        public event EventHandler ReloadTracksList;

        public TrackInfoView(TrackInfo data)
        {
            InspectedTrack = data;
            this.DataContext = InspectedTrack;
            InitializeComponent();
        }

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

    public class StringTruncationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            string inputString = value.ToString();
            return inputString.Split('\n')[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
  
}
