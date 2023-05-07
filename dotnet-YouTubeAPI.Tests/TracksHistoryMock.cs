using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeAPI;

namespace dotnet_YouTubeAPI.Tests
{
    /// <summary>
    /// Represents TracksHistory table which contains informations about its views, likes, comments and time when the video was added.
    /// </summary>
    public class TracksHistoryMock
    {
        /// <summary>
        /// Gets or sets unique ID of the entry in table. Represents primary key in TracksHistory table.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the track. Represetns foreign key for Tracks table.
        /// </summary>
        [ForeignKey("Track")]
        public string VideoId { get; set; }

        /// <summary>
        /// Gets or sets the number of likes for the track.
        /// </summary>
        public Int64 LikeCount { get; set; }

        /// <summary>
        /// Gets or sets the number of views for the track.
        /// </summary>
        public Int64 ViewCount { get; set; }

        /// <summary>
        /// Gets or sets the number of comments for the track.
        /// </summary>
        public Int64 CommentCount { get; set; }

        /// <summary>
        /// Gets or sets date and time when entry was added.
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// Connection to one track from Tracks table which represents one to many relationship.
        /// </summary>
        public virtual Track Track { get; set; }

        /// <summary>
        /// Non-parametric contructor for TracksHistory class.
        /// </summary>
        public TracksHistoryMock() { }

        /// <summary>
        /// Constructor for TracksHisotry class. It creates one instance of TracksHistory class which represents one entry in TrackHistory table with given track ID from Tracks table.
        /// </summary>
        /// <param name="Id">Track ID for which entry will be added.</param>
        public TracksHistoryMock(string Id)
        {
            VideoId = Id;
            AddTime = new DateTime(2023, 07, 05, 21, 37, 00);
            GetViedoStats();
        }

        /// <summary>
        /// Gets statistics about Track identified by VideoId using YoutubeAPI services.
        /// </summary>
        public void GetViedoStats()
        {
            ViewCount = (Int64)1234;
            LikeCount = (Int64)1234;
            CommentCount = (Int64)1234;
        }
    }
}
