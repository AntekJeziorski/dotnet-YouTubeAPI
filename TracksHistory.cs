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
    public class TracksHistory
    {
        public int ID { get; set; }
        [ForeignKey("Track")]
        public string VideoId { get; set; }
        public int LikeCount { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual Track Track { get; set; }

        public TracksHistory(string Id)
        {
            VideoId = Id;
            GetViedoStats();
        }

        public async Task GetViedoStats()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCTlONe6H40ircsdbuIq87DGV5gZeVv2wc",
                ApplicationName = this.GetType().ToString()
            });
            //string videoId = "nLIp4wd0oXs";
            // Prepare the request
            VideosResource.ListRequest listRequest = youtubeService.Videos.List("statistics");
            listRequest.Id = VideoId;
            try
            {
                // Execute the request
                VideoListResponse response = listRequest.Execute();
                // Access the video information
                foreach (var item in response.Items)
                {
                    ViewCount = (int)item.Statistics.ViewCount;
                    LikeCount = (int)item.Statistics.LikeCount;
                    CommentCount = (int)item.Statistics.CommentCount;

                    Console.WriteLine("View Count: " + ViewCount);
                    Console.WriteLine("Like Count: " + LikeCount);
                }
            }
            catch (Exception e)
            {
                // Log the error
                Console.WriteLine("An error occurred: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}
