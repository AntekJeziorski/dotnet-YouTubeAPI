using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    /// <summary>
    /// Represents object which contains complete current information about author from both Authors and AuthorsHistory table.
    /// </summary>
    public class AuthorInfo
    {
        /// <summary>
        /// Gets or sets the newest AuthorsHistory object from AuthorsHistory table.
        /// </summary>
        public AuthorsHistory AuthorsHistory { get; set; }

        /// <summary>
        /// Gets or sets Author object from Authors table.
        /// </summary>
        public Author Author { get; set; }
    }
}
