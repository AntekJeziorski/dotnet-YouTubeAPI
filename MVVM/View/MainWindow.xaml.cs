using System;
using System.Windows;
using System.Timers;


namespace YouTubeAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Timer _timer;
        public MainWindow()
        {
            InitializeComponent();
            // Create a timer with a 1 minutes interval.
            _timer = new Timer(60000);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += Update;
            // Set the timer to repeat.
            _timer.AutoReset = true;
            // Start the timer.
            _timer.Enabled = true;
        }

        public static void Update(Object source, ElapsedEventArgs e)
        {
            using (var context = new YouTubeApiContext())
            {
                context.UpdateAllAuthors();
                context.UpdateAllTracks();
            }
        }
    }
}
