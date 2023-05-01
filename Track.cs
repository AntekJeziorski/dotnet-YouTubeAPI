using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YouTubeAPI
{
    public class Track
    {
        [Key]
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string ChannelTitle { get; set; }
        public string ChannelId { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ThumbnailMedium { get; set; }

        public virtual ICollection<TracksHistory> TracksHistory { get; set; }


        public Track(string Id)
        {
            VideoId = Id;
            GetViedoData();
        }

        public async Task GetViedoData()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCTlONe6H40ircsdbuIq87DGV5gZeVv2wc",
                ApplicationName = this.GetType().ToString()
            });
            //string videoId = "nLIp4wd0oXs";
            // Prepare the request
            VideosResource.ListRequest listRequest = youtubeService.Videos.List("snippet,statistics");
            listRequest.Id = VideoId;
            try
            {
                // Execute the request
                VideoListResponse response = listRequest.Execute();
                // Access the video information
                foreach (var item in response.Items)
                {
                    //ViewCount = (int)item.Statistics.ViewCount;
                    //LikeCount = (int)item.Statistics.LikeCount;
                    Title = item.Snippet.Title;
                    ChannelTitle = item.Snippet.ChannelTitle;
                    ChannelId = item.Snippet.ChannelId;

                    Console.WriteLine("Title: " + Title);
                    Console.WriteLine("Viedo Id: " + VideoId);
                    Console.WriteLine("Channel Name: " + ChannelTitle);
                    Console.WriteLine("Channel Id: " + ChannelId);
                    //Console.WriteLine("View Count: " + ViewCount);
                    //Console.WriteLine("Like Count: " + LikeCount);

                    /*Console.WriteLine("Title: " + item.Snippet.Title);
                    Console.WriteLine("View Count: " + item.Statistics.ViewCount);*/
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
