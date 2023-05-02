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
    public class AuthorsHistory
    {
        public int ID { get; set; }
        [ForeignKey("Author")]
        public string ChannelId { get; set; }
        public Int64 ViewCount { get; set; }
        public Int64 SubCount { get; set; }
        public Int64 VideoCount { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual Author Author { get; set; }

        public AuthorsHistory() { }
        public AuthorsHistory(string Id)
        {
            try
            {
                ChannelId = Id;
                GetChannelStats();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

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
            catch (Exception ex)
            {
                // Log the error
                throw ex;
            }
            Console.ReadLine();
        }

    }
}
