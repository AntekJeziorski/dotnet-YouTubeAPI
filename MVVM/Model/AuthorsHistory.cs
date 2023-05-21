using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YouTubeAPI
{
    /// <summary>
    /// Represents AuthorsHistory table which contains informations about its subscriptions, views and number of videos.
    /// </summary>
    public class AuthorsHistory
    {
        /// <summary>
        /// Gets or sets unique ID of the entry in table. Represents primary key in TracksHistory table.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the author. Represetns foreign key for Authors table.
        /// </summary>
        [ForeignKey("Author")]
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the number of views for author.
        /// </summary>
        public Int64 ViewCount { get; set; }

        /// <summary>
        /// Gets or sets the number of subscribtions for author.
        /// </summary>
        public Int64 SubCount { get; set; }

        /// <summary>
        /// Gets or sets the number of author's videos.
        /// </summary>
        public Int64 VideoCount { get; set; }

        /// <summary>
        /// Gets or sets date and time when entry was added.
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// Connection to one author from Authors table which represents one to many relationship.
        /// </summary>
        public virtual Author Author { get; set; }

        /// <summary>
        /// Non-parametric contructor for AuthorsHistory class.
        /// </summary>
        public AuthorsHistory() { }

        /// <summary>
        /// Constructor for AuthorsHistory class. It creates one instance of AuthorsHistory class which represents one entry in AuthorsHistory table with given ChannelID from Authors table.
        /// </summary>
        /// <param name="Id"></param>
        public AuthorsHistory(string Id)
        {
            try
            {
                ChannelId = Id;
                AddTime = DateTime.Now;
                GetChannelStats();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        /// <summary>
        /// Gets statistics about Channel identified by ChannelId.
        /// </summary>
        public void GetChannelStats()
        {
            var API = new APILink();
            (ViewCount, SubCount, VideoCount) = API.GetChannelStats(ChannelId);
        }

    }
}
