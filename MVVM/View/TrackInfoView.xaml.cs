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
                series.Points.AddXY(item.AddTime, item.ViewCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Line;

            // Add the series to the chart
            ViewsChart.Series.Add(series);
            //ViewsChart.Series[0].Color = System.Drawing.Color.Blue;
            ViewsChart.Series[0].ChartType = SeriesChartType.Line;
            ViewsChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            ViewsChart.Series[0].MarkerColor = System.Drawing.Color.OrangeRed;
            ViewsChart.Series[0].ToolTip = "Time: #VALX{dd/MM/yyyy HH:mm}, Views: #VALY{#,##0;#,##0;#,##0;0}";

            // Customize the chart appearance
            ViewsChart.Titles.Add("Likes number per history update");
            ViewsChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy\nHH:mm";
            ViewsChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0;#,##0;#,##0;0";
            ViewsChart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
            ViewsChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            ViewsChart.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
            ViewsChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            ViewsChart.ChartAreas[0].AxisY.Title = "Number of likes";

            ViewsChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            ViewsChart.ChartAreas[0].CursorX.AutoScroll = true;
            ViewsChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        }

        private void PopulateLikesChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var track = context.GetTracksHistory(InspectedTrack.Track.VideoId);
            var series = new Series("Data");
            foreach (var item in track)
            {
                series.Points.AddXY(item.AddTime, item.ViewCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Line;

            // Add the series to the chart
            LikesChart.Series.Add(series);
            //LikesChart.Series[0].Color = System.Drawing.Color.Red;
            LikesChart.Series[0].ChartType = SeriesChartType.Line;
            LikesChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            LikesChart.Series[0].MarkerColor = System.Drawing.Color.OrangeRed;
            LikesChart.Series[0].ToolTip = "Time: #VALX{dd/MM/yyyy HH:mm}, Views: #VALY{#,##0;#,##0;#,##0;0}";

            // Customize the chart appearance
            LikesChart.Titles.Add("Likes number per history update");
            LikesChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy\nHH:mm";
            LikesChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0;#,##0;#,##0;0";
            LikesChart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
            LikesChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            LikesChart.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
            LikesChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            LikesChart.ChartAreas[0].AxisY.Title = "Number of views";

            LikesChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            LikesChart.ChartAreas[0].CursorX.AutoScroll = true;
            LikesChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        }

        private void PopulateCommentsChart(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
            var context = new YouTubeApiContext();
            var track = context.GetTracksHistory(InspectedTrack.Track.VideoId);
            
            var series = new Series("Data");
            foreach (var item in track)
            {
                series.Points.AddXY(item.AddTime, item.CommentCount);
            }
            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Line;

            // Add the series to the chart
            CommentsChart.Series.Add(series);
            //CommentsChart.Series[0].Color = System.Drawing.Color.Red;
            CommentsChart.Series[0].ChartType = SeriesChartType.Line;
            CommentsChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            CommentsChart.Series[0].MarkerColor = System.Drawing.Color.OrangeRed;
            CommentsChart.Series[0].ToolTip = "Time: #VALX{dd/MM/yyyy HH:mm}, Views: #VALY{#,##0;#,##0;#,##0;0}";

            // Customize the chart appearance
            CommentsChart.Titles.Add("Likes number per history update");
            CommentsChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy\nHH:mm";
            CommentsChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0;#,##0;#,##0;0";
            CommentsChart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
            CommentsChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            CommentsChart.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
            CommentsChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            CommentsChart.ChartAreas[0].AxisY.Title = "Number of likes";

            CommentsChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            CommentsChart.ChartAreas[0].CursorX.AutoScroll = true;
            CommentsChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
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
