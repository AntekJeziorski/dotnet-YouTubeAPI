using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Gets statistics about Channel identified by ChannelId using YoutubeAPI services.
        /// </summary>
        public void GetChannelStats()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCTlONe6H40ircsdbuIq87DGV5gZeVv2wc",
                ApplicationName = this.GetType().ToString()
            });
            // Prepare the request
            ChannelsResource.ListRequest listRequest = youtubeService.Channels.List("statistics");
            listRequest.Id = ChannelId;
            try
            {
                // Execute the request
                ChannelListResponse response = listRequest.Execute();
                if (response.PageInfo.TotalResults > 0)
                {
                    // Access the channel information
                    foreach (var item in response.Items)
                    {
                        ViewCount = (Int64)item.Statistics.ViewCount;
                        SubCount = (Int64)item.Statistics.SubscriberCount;
                        VideoCount = (Int64)item.Statistics.VideoCount;


                        Console.WriteLine("Channel view count: " + ViewCount);
                        Console.WriteLine("Channel subscription count: " + SubCount);
                        Console.WriteLine("Channel video count: " + VideoCount);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error
                throw ex;
            }
            Console.ReadLine();
        }

    }
}
