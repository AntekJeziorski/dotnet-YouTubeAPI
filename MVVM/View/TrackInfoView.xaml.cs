using System;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using YouTubeAPI;

namespace dotnet_YouTubeAPI.MVVM.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TrackInfoView : Window
    {
        public TrackInfo InspectedTrack;
        public TrackInfoView(TrackInfo data)
        {
            InitializeComponent();
            InspectedTrack = data;
            this.DataContext = InspectedTrack;
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
                    currentWindow.Close();
                }
                catch (DbUpdateException)
                {
                    System.Windows.Forms.MessageBox.Show("Track already deleted.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentWindow.Close();
                }
            }
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
