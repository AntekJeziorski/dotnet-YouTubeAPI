using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms.DataVisualization.Charting;

namespace YouTubeAPI
{
    /// <summary>
    /// Represents TracksHistory table which contains informations about its views, likes, comments and time when the video was added.
    /// </summary>
    public class TracksHistory
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
        public TracksHistory() { }

        /// <summary>
        /// Constructor for TracksHisotry class. It creates one instance of TracksHistory class which represents one entry in TrackHistory table with given track ID from Tracks table.
        /// </summary>
        /// <param name="Id">Track ID for which entry will be added.</param>
        public TracksHistory(string Id)
        {
            try
            {
                VideoId = Id;
                AddTime = DateTime.Now;
                GetViedoStats();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        /// <summary>
        /// Gets statistics about Track identified by VideoId.
        /// </summary>
        public void GetViedoStats()
        {
            var API = new APILink();
            (ViewCount, LikeCount, CommentCount) = API.GetViedoStats(VideoId);
        }
    }
}
