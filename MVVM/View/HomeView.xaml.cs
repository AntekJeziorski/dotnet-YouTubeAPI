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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button Test");
            var newVideo = new dotnet_YouTubeAPI.YouTubeApiHand();
            newVideo.GetViedoData();
            
            /*newID = int.Parse(textID.Text);
            newNickname = textNickname.Text;
            newYtChannelID = textYTID.Text;

            var newAuthor = new Author() { ID = newID, Nickname = newNickname, YtChannelID = newYtChannelID, JoiningDate = "04/23/22 04:34:22" };

            using (var context = new YouTubeApiContext())
            {
                context.addNewAuthor(newAuthor);
                authors = context.Authors.ToList();
            }

            authorList.ItemsSource = authors;*/

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
