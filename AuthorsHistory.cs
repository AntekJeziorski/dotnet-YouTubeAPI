using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    public class AuthorsHistory
    {
        public int ID;
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public int ViewCount { get; set; }
        public int SubCount { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
