<<<<<<< HEAD:MVVM/View/MainWindow.xaml.cs
using System;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> master:MainWindow.xaml.cs
using System.Windows;
using System.Timers;
using System.Windows.Forms;


namespace dotnet_YouTubeAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Timer for automatic updating TracksHistory table.
        /// </summary>
        private static System.Timers.Timer _timer;

        /// <summary>
        /// Non-parametric constructor which initializes MainWindow and timer.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            if(PingYoutube())
            {
                // Create a timer with a 1 minutes interval.
                _timer = new System.Timers.Timer(60000);
                // Hook up the Elapsed event for the timer. 
                _timer.Elapsed += UpdateTrackHistory;
                // Set the timer to repeat.
                _timer.AutoReset = true;
                // Start the timer.
                _timer.Enabled = true;
            }
            
        }

        /// <summary>
        /// Updates TracksHistory table for each subscribed Track.
        /// </summary>
        /// <param name="source">Event sender.</param>
        /// <param name="e">Caught event.</param>
        public static void UpdateTrackHistory(Object source, ElapsedEventArgs e)
        {
            using (var context = new YouTubeApiContext())
            {
                context.UpdateAllAuthors();
                context.UpdateAllTracks();
            }
        }

        /// <summary>
        /// Checks the connection to the youtube API, if the connection cannot be established a MessageBox is displayed indicating no connection.
        /// </summary>
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
