using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YouTubeAPI;

namespace dotnet_YouTubeAPI.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        int newID;
        string newNickname;
        string newYtChannelID;
        IList<Author> authors;

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
            var newVideo = new YouTubeAPI.Track(textVideoId.Text); /* nLIp4wd0oXs */
            //newVideo.GetViedoData();
            textVideoId.Clear();
        }

        private void Button_Click_Author(object sender, RoutedEventArgs e)
        {
            var newAuthor = new YouTubeAPI.Author(textAuthorId.Text);
        }
    }
}
