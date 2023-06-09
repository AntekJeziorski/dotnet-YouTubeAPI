﻿using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouTubeAPI
{
    /// <summary>
    /// Represents Tracks table which contains main information about youtube video.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Gets or sets unique VideoID of the track in table. Represents primary key in Tracks table. Contains unique string generated by YouTube.
        /// </summary>
        [Key]
        public string VideoId { get; set; }

        /// <summary>
        /// Gets or sets Title of the track.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets Channel Title on which Video was uploaded.
        /// </summary>
        public string ChannelTitle { get; set; }

        /// <summary>
        /// Gets or Sets YouTube Channel ID.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or Sets YouTube video description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets YouTube video release date.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or Sets URL for medium video thumbnail.
        /// </summary>
        public string ThumbnailMedium { get; set; }
        public DateTime SubscribeTime { get; set; }

        /// <summary>
        /// Connection to many entries in TracksHistory table which represents one to many relationship.
        /// </summary>
        public virtual ICollection<TracksHistory> TracksHistory { get; set; }

        /// <summary>
        /// Non-parametric construcor for Track class.
        /// </summary>
        public Track() { }

        /// <summary>
        /// Contructor for Track class. Creates new instance of Track class which represents one entry in Tracks table.
        /// </summary>
        /// <param name="Id">Youtube video ID.</param>
        public Track(string Id)
        {
            try
            {
                VideoId = Id;
                GetViedoData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            
        }

        /// <summary>
        /// Gets Track data identified by VideoId.
        /// </summary>
        public void GetViedoData()
        {
            var API = new APILink();
            (Title, ChannelTitle, ChannelId, Description, ReleaseDate, ThumbnailMedium) = API.GetViedoData(VideoId);
        }
    }
}
