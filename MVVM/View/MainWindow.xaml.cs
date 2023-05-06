using System;
using System.Windows;
using System.Timers;
using System.Windows.Forms;


namespace YouTubeAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static System.Timers.Timer _timer;
        public MainWindow()
        {
            InitializeComponent();
            if(PingYoutube())
            {
                // Create a timer with a 1 minutes interval.
                _timer = new System.Timers.Timer(60000);
                // Hook up the Elapsed event for the timer. 
                _timer.Elapsed += Update;
                // Set the timer to repeat.
                _timer.AutoReset = true;
                // Start the timer.
                _timer.Enabled = true;
            }
            
        }

        public static void Update(Object source, ElapsedEventArgs e)
        {
            using (var context = new YouTubeApiContext())
            {
                context.UpdateAllAuthors();
                context.UpdateAllTracks();
            }
        }

        public bool PingYoutube()
        {
            try
            {
                using (var ping = new System.Net.NetworkInformation.Ping())
                {
                    var reply = ping.Send("142.250.203.132", 2000);
                    return true;
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Couldnt reach Youtube Api.\nPeriodic update diabled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
