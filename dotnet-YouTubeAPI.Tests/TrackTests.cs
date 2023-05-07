using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using YouTubeAPI;
using System.Threading.Channels;
using System.Data.Entity.Infrastructure;

namespace dotnet_YouTubeAPI.Tests
{
    [TestFixture]
    public class TrackTests
    {
        [Test]
        public void CreateTrack_ById()
        {
            // Arrange & Act
            string videoId = "valid_mock_id";
            var track = new TrackMock(videoId);

            // Assert
            Assert.AreEqual(track.VideoId, "valid_mock_id");
            Assert.AreEqual(track.Title, "Mock Title");
            Assert.AreEqual(track.ChannelTitle, "Mock Channel Title");
            Assert.AreEqual(track.ChannelId, "Mock Channel Id");
            Assert.AreEqual(track.Description, "Mock Description");
            Assert.AreEqual(track.ReleaseDate, new DateTime(2023, 07, 05, 12, 00, 00));
            Assert.AreEqual(track.ThumbnailMedium, "Mock Thumbnail Url");
            Assert.AreEqual(track.SubscribeTime, new DateTime(2023, 07, 05, 21, 37, 00));
        }

        [Test]
        public void GetVideoData_ValidId_ReturnsData()
        {
            // Arrange
            string videoId = "valid_mock_id";
            var track = new TrackMock();
            track.VideoId = videoId;

            // Act
            track.GetViedoData();

            // Assert
            Assert.AreEqual(track.VideoId, videoId);
            Assert.AreEqual(track.Title, "Mock Title");
            Assert.AreEqual(track.ChannelTitle, "Mock Channel Title");
            Assert.AreEqual(track.ChannelId, "Mock Channel Id");
            Assert.AreEqual(track.Description, "Mock Description");
            Assert.AreEqual(track.ReleaseDate, new DateTime(2023, 07, 05, 12, 00, 00));
            Assert.AreEqual(track.ThumbnailMedium, "Mock Thumbnail Url");
            Assert.AreEqual(track.SubscribeTime, new DateTime(2023, 07, 05, 21, 37, 00));

        }

        [Test]
        public void GetVideoData_InvalidId_ThrowsException()
        {
            // Arrange
            string videoId = "invalid_id";

            // Act & Assert
            Assert.Throws<Exception>(() => new YouTubeAPI.Track("invalid_id"));
        }

        [Test]
        public void CreateTracksHistory_ById()
        {
            // Arrange & Act
            string videoId = "valid_mock_id";
            var tracksHistory = new TracksHistoryMock(videoId);

            // Assert
            Assert.AreEqual(tracksHistory.VideoId, videoId);
            Assert.AreEqual(tracksHistory.ViewCount, 1234);
            Assert.AreEqual(tracksHistory.LikeCount, 1234);
            Assert.AreEqual(tracksHistory.CommentCount, 1234);
            Assert.AreEqual(tracksHistory.AddTime, new DateTime(2023, 07, 05, 21, 37, 00));
        }

        [Test]
        public void CreateTracksHistory_InvalidId()
        {
            // Arrange & Act
            string videoId = "invalid_id";
            var tracksHistory = new TracksHistory();
            tracksHistory.VideoId = videoId;

            // Assert
            Assert.AreEqual(tracksHistory.VideoId, videoId);
            Assert.AreEqual(tracksHistory.ViewCount, 0);
            Assert.AreEqual(tracksHistory.LikeCount, 0);
            Assert.AreEqual(tracksHistory.CommentCount, 0);
            Assert.AreEqual(tracksHistory.AddTime, new DateTime(0001, 01, 01, 00, 00, 00));
        }

        [Test]
        public void GetVideoStats_ValidId_ReturnsData()
        {
            // Arrange
            string videoId = "invalid_id";
            var tracksHistory = new TracksHistoryMock();
            tracksHistory.VideoId = videoId;

            // Act
            tracksHistory.GetViedoStats();

            // Assert
            Assert.AreEqual(tracksHistory.VideoId, videoId);
            Assert.AreEqual(tracksHistory.ViewCount, 1234);
            Assert.AreEqual(tracksHistory.LikeCount, 1234);
            Assert.AreEqual(tracksHistory.CommentCount, 1234);
            Assert.AreEqual(tracksHistory.AddTime, new DateTime(0001, 01, 01, 00, 00, 00));
        }
    }
}

