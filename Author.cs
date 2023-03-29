using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    public class Author
    {
        public int ID { get; set; }
        public string Nickname { get; set; }
        public string YtChannelID { get; set; }
        public string JoiningDate { get; set; }

        
        public virtual ICollection<AuthorsHistory> AuthorsHistory { get; set; }
        public virtual ICollection<Track> Track { get; set; }
    }
}
