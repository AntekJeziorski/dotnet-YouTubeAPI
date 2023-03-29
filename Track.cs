using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    public class Track
    { 
        public int ID { get; set; }
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public string YtClipID { get; set; }
        public string ReleaseDate { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<TracksHistory> TracksHistory { get; set; }
    }
}
