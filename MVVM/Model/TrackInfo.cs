using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    /// <summary>
    /// Represents object which contains complete current information about Track from both Tracks and Trackshistory table.
    /// </summary>
    public class TrackInfo
    {
        /// <summary>
        /// Gets or sets the newest TracksHistory object from TracksHistory table.
        /// </summary>
        public TracksHistory TracksHistory { get; set; }

        /// <summary>
        /// Gets or sets Track object from Tracks table.
        /// </summary>
        public Track Track { get; set; }
    }
}
