using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YouTubeAPI
{
    public class TracksHistory
    {
        public int ID { get; set; }
        [ForeignKey("Track")]
        public string VideoId { get; set; }
        public Int64 LikeCount { get; set; }
        public Int64 ViewCount { get; set; }
        public Int64 CommentCount { get; set; }
        public DateTime AddTime { get; set; }

        public virtual Track Track { get; set; }

        public TracksHistory() { }

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

        public void GetViedoStats()
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
                    ViewCount = (Int64)item.Statistics.ViewCount;
                    LikeCount = (Int64)item.Statistics.LikeCount;
                    CommentCount = (Int64)item.Statistics.CommentCount;

                    Console.WriteLine("View Count: " + ViewCount);
                    Console.WriteLine("Like Count: " + LikeCount);
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
