using System.Collections.Generic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Service.Interfaces;
using Web.Controllers;
using Web.Hubs;
using Web.ViewModels.Home;
using Xunit;

namespace Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexResultTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void IndexModelTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.Index() as ViewResult;
            
            Assert.IsAssignableFrom<SearchViewModel>(result?.Model);
        }
        
        [Fact]
        public void IndexModelEqualityTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.Index() as ViewResult;
            
            var model = Assert.IsAssignableFrom<SearchViewModel>(result.Model);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public void AlbumResultTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.AlbumRepository.Get(1)).Returns(GetTestAlbum());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            var result = controller.Album(1);
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void AlbumModelTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.AlbumRepository.Get(1)).Returns(GetTestAlbum());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.Album(1) as ViewResult;
            
            Assert.IsAssignableFrom<Album>(result?.Model);
        }

        [Fact]
        public void ArtistResultTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.ArtistRepository.Get(1)).Returns(GetTestArtist());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            var result = controller.Artist(1);
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ArtistModelTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.ArtistRepository.Get(1)).Returns(GetTestArtist());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.Artist(1) as ViewResult;
            
            Assert.IsAssignableFrom<Artist>(result?.Model);
        }
        
        [Fact]
        public void AddTagResultTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            var result = controller.AddTag(1);
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddTagModelTypeTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void SearchFoundTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void SearchNotFoundTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        public static List<Song> GetTestSongs()
        {
            return new List<Song>
            {
                new Song { Id = 1, Name = "1"},
                new Song {Id = 2, Name = "2"}
            };
        }
        
        public static Album GetTestAlbum()
        {
            return new Album {Id = 1, Name = "1"};
        }
        
        public static Artist GetTestArtist()
        {
            return new Artist {Id = 1, Name = "1"};
        }
        
        public static List<Tag> GetTestTags()
        {
            return new List<Tag>
            {
                new Tag { Id = 1, Name = "1"},
                new Tag {Id = 2, Name = "2"}
            };
        }
        
        public static Tag GetTestTag()
        {
            return new Tag {Id = 1, Name = "1"};
        }
    }
}