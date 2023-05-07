using System;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Windows;
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
                series.Points.AddXY(item.AddTime, item.ViewCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Line;

            // Add the series to the chart
            ViewsChart.Series.Add(series);

            ViewsChart.Series[0].ChartType = SeriesChartType.Line;
            ViewsChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            ViewsChart.Series[0].MarkerColor = System.Drawing.Color.OrangeRed;
            ViewsChart.Series[0].ToolTip = "Time: #VALX{dd/MM/yyyy HH:mm}, Views: #VALY{#,##0;#,##0;#,##0;0}";

            // Customize the chart appearance
            ViewsChart.Titles.Add("Views count history");
            ViewsChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy\nHH:mm";
            ViewsChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0;#,##0;#,##0;0";
            ViewsChart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
            ViewsChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            ViewsChart.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
            ViewsChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            ViewsChart.ChartAreas[0].AxisY.Title = "Number of views";

            // Set the chart's font sizes
            ViewsChart.Titles[0].Font = new Font("Arial", 16);
            ViewsChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12);

            ViewsChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            ViewsChart.ChartAreas[0].CursorX.AutoScroll = true;
            ViewsChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
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
                series.Points.AddXY(item.AddTime, item.SubCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Line;

            // Add the series to the chart
            SubChart.Series.Add(series);

            SubChart.Series[0].ChartType = SeriesChartType.Line;
            SubChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            SubChart.Series[0].MarkerColor = System.Drawing.Color.OrangeRed;
            SubChart.Series[0].ToolTip = "Time: #VALX{dd/MM/yyyy HH:mm}, Subscribers: #VALY{#,##0;#,##0;#,##0;0}";

            // Customize the chart appearance
            SubChart.Titles.Add("Subscribers count history");
            SubChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy\nHH:mm";
            SubChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0;#,##0;#,##0;0";
            SubChart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
            SubChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            SubChart.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
            SubChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            SubChart.ChartAreas[0].AxisY.Title = "Number of subscribers";

            // Set the chart's font sizes
            SubChart.Titles[0].Font = new Font("Arial", 16);
            SubChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12);

            SubChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            SubChart.ChartAreas[0].CursorX.AutoScroll = true;
            SubChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
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
                series.Points.AddXY(item.AddTime, item.VideoCount);
            }

            // Set the chart type to scatter
            series.ChartType = SeriesChartType.Line;

            // Add the series to the chart
            VideosChart.Series.Add(series);

            VideosChart.Series[0].ChartType = SeriesChartType.Line;
            VideosChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            VideosChart.Series[0].MarkerColor = System.Drawing.Color.OrangeRed;
            VideosChart.Series[0].ToolTip = "Time: #VALX{dd/MM/yyyy HH:mm}, Videos: #VALY{#,##0;#,##0;#,##0;0}";

            // Customize the chart appearance
            VideosChart.Titles.Add("Videos count history");
            VideosChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy\nHH:mm";
            VideosChart.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0;#,##0;#,##0;0";
            VideosChart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
            VideosChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            VideosChart.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
            VideosChart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            VideosChart.ChartAreas[0].AxisY.Title = "Number of videos";

            // Set the chart's font sizes
            VideosChart.Titles[0].Font = new Font("Arial", 16);
            VideosChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12);

            VideosChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            VideosChart.ChartAreas[0].CursorX.AutoScroll = true;
            VideosChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        }
    }
}

