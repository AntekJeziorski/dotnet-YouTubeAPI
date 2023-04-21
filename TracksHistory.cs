using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    public class TracksHistory
    {
        public int ID { get; set; }
        [ForeignKey("Track")]
        public string TrackID { get; set; }
        public int LikeCount { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual Track Track { get; set; }
    }
}
